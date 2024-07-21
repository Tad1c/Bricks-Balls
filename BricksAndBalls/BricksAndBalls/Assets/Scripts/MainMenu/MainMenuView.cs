using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
	[SerializeField] private Button playButton;
	[SerializeField] private Button leaderBoardButton;
	[SerializeField] private TMP_Text levelNumberTxt;
	[SerializeField] private TMP_Text playerNameTxt;


	public event Action OnPlayButtonClicked;
	public event Action OnLeaderBoardButtonClicked;

	public void Start()
	{
		playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
		leaderBoardButton.onClick.AddListener(() => OnLeaderBoardButtonClicked?.Invoke());
	}

	public void SetLevelTxt(int levelNumber)
	{
		levelNumberTxt.SetText($"Level {levelNumber}");
	}

	public void SetPlayerNameTxt(string name)
	{
		playerNameTxt.SetText($"Hello {name}");
	}
	
}
