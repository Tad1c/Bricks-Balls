using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelEditorWindow : EditorWindow
{
	private PrefabCollection prefabCollection;
	private GameObject selectedPrefab;
	private Vector2 scrollPosition;
	private GameObject levelParent;
	private int gridButtonSize = 50;
	private Vector2 prefabScrollPosition;
	private Vector2 levelObjectScrollPosition;
	private BoardManagerEditor boardManagerEditor;

	private int rows = 10;
	private int columns = 10;
	private float spacing = 1.0f;
	private float verticalOffset = 0f;
	private float horizontalOffset = 0f;
	private float randomDecider = 0.5f;
	private int minHealth = 1;
	private int maxHealth = 5;

	private bool showRandomLevelSettings = true;


	[MenuItem("Window/Level Editor")]
	public static void ShowWindow()
	{
		GetWindow<LevelEditorWindow>("Level Editor");
	}

	private void OnEnable()
	{
		FindBoardParent();
		EditorApplication.update += OnEditorUpdate;
	}

	private void OnDisable()
	{
		EditorApplication.update -= OnEditorUpdate;
	}

	private void OnEditorUpdate()
	{
		if (levelParent == null)
		{
			FindBoardParent();
		}
	}

	private void FindBoardParent()
	{
		levelParent = GameObject.Find("Board");
		Undo.RegisterCreatedObjectUndo(levelParent, "Create Board");
	}

	private void OnGUI()
	{
		GUILayout.Label("Level Editor", EditorStyles.boldLabel);

		if (levelParent == null)
		{
			GUILayout.Label("Missing 'Board' GameObject", EditorStyles.boldLabel);
			GUILayout.Label("Please create a GameObject named 'Board' in the scene.",
				new GUIStyle { normal = new GUIStyleState { textColor = Color.red } });

			if (GUILayout.Button("Create Board"))
			{
				levelParent = new GameObject("Board");
			}

			return;
		}

		showRandomLevelSettings = EditorGUILayout.Foldout(showRandomLevelSettings, "Random Level Settings");

		if (showRandomLevelSettings)
		{
			GUILayout.BeginVertical("box");
			rows = EditorGUILayout.IntField("Rows", rows);
			columns = EditorGUILayout.IntField("Columns", columns);
			spacing = EditorGUILayout.FloatField("Spacing", spacing);
			verticalOffset = EditorGUILayout.FloatField("Vertical Offset", verticalOffset);
			horizontalOffset = EditorGUILayout.FloatField("Horizontal Offset", horizontalOffset);
			randomDecider = EditorGUILayout.Slider("Random Space Decider", randomDecider, 0, 1);
			minHealth = EditorGUILayout.IntField("Min Health", minHealth);
			maxHealth = EditorGUILayout.IntField("Max Health", maxHealth);
			GUILayout.EndVertical();
		}

		if (GUILayout.Button("Generate Random Level"))
		{
			GenerateRandomLevel();
		}


		DisplayPrefabCollection();

		DisplayLevelObjects();

		GUILayout.Space(10);

		if (GUILayout.Button("Save Level"))
		{
			SaveLevel();
		}

		if (GUILayout.Button("Load Level"))
		{
			LoadLevel();
		}
	}

	private void DisplayLevelObjects()
	{
		GUILayout.Label("Level Objects", EditorStyles.boldLabel);
		levelObjectScrollPosition = GUILayout.BeginScrollView(levelObjectScrollPosition, GUILayout.Height(200));

		if (levelParent != null)
		{
			foreach (Transform child in levelParent.transform)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label(child.name);
				if (GUILayout.Button("Select"))
				{
					Selection.activeGameObject = child.gameObject;
				}

				if (GUILayout.Button("Remove"))
				{
					Undo.DestroyObjectImmediate(child.gameObject);
				}

				GUILayout.EndHorizontal();
			}
		}

		GUILayout.EndScrollView();
	}

	private void DisplayPrefabsInGrid()
	{
		float windowWidth = position.width;
		int gridColumns = Mathf.Max(1, Mathf.FloorToInt(windowWidth / gridButtonSize));
		int count = 0;
		GUILayout.BeginHorizontal();
		foreach (PrefabItem item in prefabCollection.PrefabItems)
		{
			if (count % gridColumns == 0 && count != 0)
			{
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
			}

			GUILayout.BeginVertical(GUILayout.Width(gridButtonSize), GUILayout.Height(gridButtonSize));
			if (GUILayout.Button(item.icon, GUILayout.Width(gridButtonSize), GUILayout.Height(gridButtonSize)))
			{
				selectedPrefab = item.prefab;
				PlaceObject(selectedPrefab);
			}

			GUILayout.Label(item.Name);
			GUILayout.EndVertical();

			count++;
		}

		GUILayout.EndHorizontal();
	}

	private void DisplayPrefabCollection()
	{
		GUILayout.BeginVertical();
		prefabCollection = Resources.Load<PrefabCollection>("PrefabCollection");

		if (prefabCollection != null)
		{
			GUILayout.Label("Select a Prefab", EditorStyles.boldLabel);
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(100));

			DisplayPrefabsInGrid();

			GUILayout.EndScrollView();
		}

		GUILayout.EndVertical();
	}


	private GameObject PlaceObject(GameObject prefab, Vector3 pos = default)
	{
		if (prefab != null)
		{
			GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
			Undo.RegisterCreatedObjectUndo(obj, "Place Object");
			obj.transform.position = pos;
			if (levelParent != null)
			{
				obj.transform.parent = levelParent.transform;
			}

			return obj;
		}

		return null;
	}

	private void SaveLevel()
	{
		if (levelParent == null)
		{
			Debug.LogError("Level parent not set.");
			return;
		}

		LevelData levelData = CreateInstance<LevelData>();

		foreach (Transform child in levelParent.transform)
		{
			Brick brick = child.GetComponent<Brick>();
			if (brick)
			{
				ObjectData objData = new ObjectData();
				objData.PrefabName = PrefabUtility.GetCorrespondingObjectFromSource(child.gameObject).name;
				objData.Position = child.position;
				objData.Rotation = child.rotation;
				objData.Scale = child.localScale;
				objData.Health = brick.Health;

				levelData.Objects.Add(objData);
			}
		}

		string path = EditorUtility.SaveFilePanelInProject("Save Level Data", "NewLevelData", "asset",
			"Please enter a file name to save the level data to");

		if (path == "")
		{
			return;
		}
		SetLevelDataToBoard(levelData);
		AssetDatabase.CreateAsset(levelData, path);
		AssetDatabase.SaveAssets();
	}

	private void LoadLevel()
	{
		string path = EditorUtility.OpenFilePanel("Select Level", Application.dataPath, "");
		if (File.Exists(path))
		{
			path = "Assets" + path.Substring(Application.dataPath.Length);

			LevelData levelData = AssetDatabase.LoadAssetAtPath<LevelData>(path);

			if (levelParent != null)
			{
				ClearLevel();
			}

			int score = 0;

			foreach (ObjectData objData in levelData.Objects)
			{
				Brick prefab = Resources.Load<Brick>(objData.PrefabName);

				if (prefab != null)
				{
					Brick obj = (Brick)PrefabUtility.InstantiatePrefab(prefab);
					obj.transform.position = objData.Position;
					obj.transform.rotation = objData.Rotation;
					obj.transform.localScale = objData.Scale;
					obj.SetHealth(objData.Health);
					score += obj.Health;
					if (levelParent != null)
					{
						obj.transform.parent = levelParent.transform;
					}
				}
			}

			Debug.Log("Level loaded from " + path);
			Debug.Log($"Total score {score}");
			SetLevelDataToBoard(levelData);
		}
		else
		{
			Debug.LogError("No level file found at " + path);
		}
	}

	private void SetLevelDataToBoard(LevelData levelData)
	{
		if (!boardManagerEditor)
		{
			boardManagerEditor = levelParent.GetComponent<BoardManagerEditor>();
		}
		
		boardManagerEditor.SetLevelData(levelData);
	}

	private void GenerateRandomLevel()
	{
		if (prefabCollection == null || levelParent == null)
		{
			return;
		}

		Vector3 boardCenter = levelParent.transform.position;
		Vector3 offset = new Vector3(horizontalOffset, verticalOffset, 0);

		ClearLevel();

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				if (Random.Range(0f, 1f) > randomDecider)
				{
					PrefabItem randomPrefab =
						prefabCollection.PrefabItems[Random.Range(0, prefabCollection.PrefabItems.Count)];
					Vector3 position = boardCenter + offset +
					                   new Vector3((j - columns / 2) * spacing, (i - rows / 2) * spacing, 0);
					GameObject obj = PlaceObject(randomPrefab.prefab, position);

					Brick brick = obj.GetComponent<Brick>();
					if (brick != null)
					{
						int randomHealth = Random.Range(minHealth, maxHealth + 1);
						brick.SetHealth(randomHealth);
					}
				}
			}
		}
	}

	private void ClearLevel()
	{
		while (levelParent.transform.childCount > 0)
		{
			DestroyImmediate(levelParent.transform.GetChild(0).gameObject);
		}
	}
}