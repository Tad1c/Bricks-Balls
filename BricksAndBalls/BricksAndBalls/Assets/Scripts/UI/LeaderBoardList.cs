using System;
using System.Collections.Generic;
using UIS;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class LeaderBoardList : MonoBehaviour
{
	[SerializeField] private Scroller List = null;
	[SerializeField] private GameObject panel;

	[SerializeField] private int Count = 100;

	private readonly List<EntryData> list = new List<EntryData>();
	private IPlayerStorage playerStorage;

	private EntryData playerEntryData;

	[Inject]
	public void Constructor(IPlayerStorage playerStorage)
	{
		this.playerStorage = playerStorage;
	}

	private void Start()
	{
		for (int i = 0; i < Count - 1; i++)
		{
			EntryData entryData = new()
			{
				Name = $"Player {i + 1}",
				Score = Random.Range(500, 10000),
			};

			list.Add(entryData);
		}

		PlayerData playerData = playerStorage.GetPlayerData();
		playerEntryData = new EntryData()
		{
			Name = playerData.PlayerName,
			Score = playerData.Score,
			IsLocalPlayer = true
		};

		list.Add(playerEntryData);

		list.Sort(((data, data1) => data1.Score.CompareTo(data.Score)));

		List.InitData(Count);
		List.ScrollTo(list.IndexOf(playerEntryData));
	}

	private void OnEnable()
	{
		List.OnFill += OnFillItem;
		List.OnHeight += OnHeightItem;

		if (list.Count > 0)
		{
			List.ScrollTo(list.IndexOf(playerEntryData));
		}
	}

	private void OnDisable()
	{
		List.OnFill -= OnFillItem;
		List.OnHeight -= OnHeightItem;
	}

	private void OnFillItem(int index, GameObject item)
	{
		LeaderBoardEntry leaderBoardEntry = item.GetComponent<LeaderBoardEntry>();
		leaderBoardEntry.SetTexts(list[index], index + 1);
	}

	public void CloseLeaderBoard()
	{
		gameObject.SetActive(false);
	}

	private int OnHeightItem(int index)
	{
		return 150;
	}
}