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

	public List<Button> MultiplierButtons => multiplierButtons;
	public void SetHomeButton(Action onClick)
	{
		homeButton.onClick.AddListener(() => onClick?.Invoke());
	}
	
	public void SetWinPanel(int score, int totalScore)
	{
		scoreTxt.SetText($"Current Score {score}");
		totalScoreTxt.SetText($"Total Score {totalScore}");
	}
}
