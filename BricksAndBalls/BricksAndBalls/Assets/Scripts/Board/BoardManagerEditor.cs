using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class BoardManagerEditor : MonoBehaviour, IBoardManager
{
	[SerializeField] private LevelData levelData;
	private IObjectResolver resolver;
	private List<Brick> bricks;

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

	public void MoveDownBricks()
	{
		foreach (Brick brick in bricks)
		{
			brick.transform.position += Vector3.down;
		}
	}

	private void CreateBrick(Vector2 position, int health, string prefabName)
	{
		Brick prefab = Resources.Load<Brick>(prefabName);
		if (prefab != null)
		{
			Brick brick = Instantiate(prefab, position, Quaternion.identity, transform);
			resolver.Inject(brick);
			brick.SetHealth(health);
			bricks.Add(brick);
		}
		else
		{
			Debug.LogError($"Prefab '{prefabName}' not found in Resources.");
		}
	}

	private void ClearBoard()
	{
		while (transform.childCount > 0)
		{
			Destroy(transform.GetChild(0).gameObject);
		}
	}
}