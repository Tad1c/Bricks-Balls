using System;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelView : MonoBehaviour
{
	[SerializeField] private Button homeButton;
	[SerializeField] private Button retryButton;
	[SerializeField] private Button leaderBoardButton;

	public void InitButtons(Action onHome, Action onRetry, Action onLeaderboard)
	{
		homeButton.onClick.AddListener(() => onHome?.Invoke());
		retryButton.onClick.AddListener(() => onRetry?.Invoke());
		leaderBoardButton.onClick.AddListener(() => onLeaderboard?.Invoke());
	}
}
