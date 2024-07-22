using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class BoardManagerEditor : MonoBehaviour, IBoardManager
{
	[SerializeField] private LevelData levelData;
	private IObjectResolver resolver;
	private List<Brick> bricks = new List<Brick>();

	private int totalBricks;
	private GamePlayEvents gamePlayEvents;

	[Inject]
	public void Constructor(IObjectResolver resolver, GamePlayEvents gamePlayEvents)
	{
		this.gamePlayEvents = gamePlayEvents;
		this.resolver = resolver;
	}

	private void Start()
	{
		LoadBoard(levelData);
	}

	public void SetLevelData(LevelData levelData)
	{
		this.levelData = levelData;
	}

	public void LoadBoard(LevelData levelData)
	{
		ClearBoard();

		foreach (ObjectData brickData in levelData.Objects)
		{
			CreateBrick(brickData.Position, brickData.Health, brickData.PrefabName);
		}

		totalBricks = bricks.Count;
	}

	public void MoveDownBricks()
	{
		foreach (Brick brick in bricks)
		{
			brick.transform.position += Vector3.down;
		}
	}

	public void ReduceBricks()
	{
		totalBricks--;
		if (totalBricks == 0)
		{
			gamePlayEvents.OnGameWin?.Invoke();
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
		bricks.Clear();
		foreach (Brick brick in FindObjectsOfType<Brick>())
		{
			Destroy(brick.gameObject);
		}
	}
}