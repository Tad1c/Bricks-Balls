using UnityEngine;
using VContainer;

public class BoardManagerEditor : MonoBehaviour, IBoardManager
{

	[SerializeField] private LevelData levelData;
	private IObjectResolver resolver;

	[Inject]
	public void Constructor(IObjectResolver resolver)
	{
		this.resolver = resolver;
	}

	public void SetLevelData(LevelData levelData)
	{
		this.levelData = levelData;
	}

	private void Start()
	{
		if (!Application.isEditor || Application.isPlaying)
		{
			LoadBoard(levelData);
		}
	}

	public void LoadBoard(LevelData levelData)
	{
		ClearBoard();

		foreach (ObjectData brickData in levelData.Objects)
		{
			CreateBrick(brickData.Position, brickData.Health, brickData.PrefabName);
		}
	}

	public void Test()
	{
		Debug.Log("Test Succeed");
	}

	private void CreateBrick(Vector2 position, int health, string prefabName)
	{
		Brick prefab = Resources.Load<Brick>(prefabName); // Load the prefab from Resources
		if (prefab != null)
		{
			Brick brick = Instantiate(prefab, position, Quaternion.identity, transform);
			resolver.Inject(brick);
			brick.SetHealth(health);
		}
		else
		{
			Debug.LogError($"Prefab '{prefabName}' not found in Resources.");
		}
	}

	private void ClearBoard()
	{
		foreach (Brick brick in FindObjectsOfType<Brick>())
		{
			Destroy(brick.gameObject);
		}
	}
}