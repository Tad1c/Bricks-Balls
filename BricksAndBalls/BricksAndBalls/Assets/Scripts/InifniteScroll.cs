using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InifniteScroll : MonoBehaviour
{
	public LeaderBoardEntry leaderboardItemPrefab;
	public Transform content;
	public int numberOfPlayers = 100;
	public int visibleItemCount = 10;
	public Color userColor;

	private List<LeaderBoardEntry> leaderboardItems = new List<LeaderBoardEntry>();
	private List<int> scores = new List<int>();
	private RectTransform contentRect;

	void Start()
	{
		contentRect = content.GetComponent<RectTransform>();
		InitializeScores();
		InitializeItems();
		UpdateItems();
	}

	void InitializeScores()
	{
		for (int i = 0; i < numberOfPlayers; i++)
		{
			scores.Add(Random.Range(100, 1000));
		}
	}

	void InitializeItems()
	{
		for (int i = 0; i < visibleItemCount; i++)
		{
			LeaderBoardEntry newItem = Instantiate(leaderboardItemPrefab, content);
			leaderboardItems.Add(newItem);
		}
	}

	void UpdateItems()
	{
		float itemHeight = leaderboardItemPrefab.GetComponent<RectTransform>().rect.height;
		float scrollPosition = contentRect.anchoredPosition.y;
		int startIndex = Mathf.Clamp((int)(scrollPosition / itemHeight), 0, numberOfPlayers - visibleItemCount);

		for (int i = 0; i < visibleItemCount; i++)
		{
			int index = startIndex + i;
			if (index < numberOfPlayers)
			{
				UpdateEntry(leaderboardItems[i], index);
				//leaderboardItems[i].Setup(index + 1, "Player " + (index + 1), scores[index]);
				// if (IsUserPosition(index))
				// {
				// 	leaderboardItems[i].Highlight(userColor);
				// }
				// else
				// {
				// 	leaderboardItems[i].ResetHighlight();
				// }
			}
		}
	}

	bool IsUserPosition(int index)
	{
		// Replace this with the actual logic to determine if the index is the user's position
		return index == 0; // Example: assuming the user is at the first position
	}

	void Update()
	{
		UpdateItems();
	}


	private void UpdateEntry(LeaderBoardEntry entry, int index)
	{
		entry.SetTexts($"Player {index + 1}", Random.Range(0, 1000));
	}
}