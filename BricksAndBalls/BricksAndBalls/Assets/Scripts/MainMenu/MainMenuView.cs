using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
	[SerializeField] private Button playButton;
	[SerializeField] private Button leaderBoardButton;
	[SerializeField] private Button signUpButton;
	[SerializeField] private TMP_Text levelNumberTxt;
	[SerializeField] private TMP_Text playerNameTxt;


	[SerializeField] private GameObject enterNamePanel;
	
	
	private string playerName;

	public event Action OnPlayButtonClicked;
	public event Action OnLeaderBoardButtonClicked;
	public event Action<string> OnNewPlayerSignUp;

	public void Start()
	{
		playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
		leaderBoardButton.onClick.AddListener(() => OnLeaderBoardButtonClicked?.Invoke());
		signUpButton.onClick.AddListener(() =>
		{
			OnNewPlayerSignUp?.Invoke(playerName);
			enterNamePanel.SetActive(false);
		});
	}

	public void ToggleMainMenuView(bool toggle)
	{
		gameObject.SetActive(toggle);
	}

	public void SetLevelTxt(int levelNumber)
	{
		levelNumberTxt.SetText($"Level {levelNumber}");
	}

	public void SetPlayerNameTxt(string name)
	{
		playerNameTxt.SetText($"Hello {name}");
	}

	public void ShowNameInputPanel()
	{
		signUpButton.interactable = false;
		enterNamePanel.SetActive(true);
	}

	public void InputPlayerName(string input)
	{
		signUpButton.interactable = !string.IsNullOrEmpty(input);
		playerName = input;
	}
	
}
