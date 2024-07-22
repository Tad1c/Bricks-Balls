using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelView : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreTxt;
	[SerializeField] private TMP_Text totalScoreTxt;
	[SerializeField] private List<Button> multiplierButtons;
	[SerializeField] private Button homeButton;
	[SerializeField] private Button leaderBoardButton;

	public List<Button> MultiplierButtons => multiplierButtons;
	public void InitButtons(Action onHomeClick, Action onLeaderboardClick)
	{
		homeButton.onClick.AddListener(() => onHomeClick?.Invoke());
		leaderBoardButton.onClick.AddListener(() => onLeaderboardClick?.Invoke());
	}
	
	public void SetWinPanel(int score, int totalScore)
	{
		scoreTxt.SetText($"Current Score {score}");
		totalScoreTxt.SetText($"Total Score {totalScore}");
	}
}
