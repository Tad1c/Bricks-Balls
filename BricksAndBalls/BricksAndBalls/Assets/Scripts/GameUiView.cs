using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUiView : MonoBehaviour
{

	[Header("Win Panel")]
	[SerializeField] private WinPanelView winPanelView;
	[Space(10)]
	[Header("Lose Panel")]
	[SerializeField] private LosePanelView losePanel;

	public event Action<int> OnMultiplierButtonClicked;
	public event Action OnHomeButtonClicked;
	public event Action OnRetryButtonClicked; 

	private void Awake()
	{
		winPanelView.SetHomeButton(OnHomeButtonClicked);
		losePanel.SetButtons(OnHomeButtonClicked, OnRetryButtonClicked);
	}

	public void ShowLosePanel()
	{
		losePanel.gameObject.SetActive(true);
	}

	public void ShowWinPanel(int score, int totalScore)
	{
		winPanelView.gameObject.SetActive(true);
		winPanelView.SetWinPanel(score, totalScore);
	}

	public void MultiplierButton(int multiplier)
	{
		foreach (Button button in winPanelView.MultiplierButtons)
		{
			button.interactable = false;
		}

		OnMultiplierButtonClicked?.Invoke(multiplier);
	}
}
