using System;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class MainMenuPresenter: IStartable, IDisposable
{
	private readonly MainMenuView view;
	private readonly IPlayerStorage playerStorage;

	public MainMenuPresenter(MainMenuView view, IPlayerStorage playerStorage)
	{
		this.view = view;
		this.playerStorage = playerStorage;
	}

	public void Start()
	{
		PlayerData playerData = playerStorage.GetPlayerData();
		if (string.IsNullOrEmpty(playerData.PlayerName))
		{
			view.ShowNameInputPanel();
		}
		else
		{
			UpdateView(playerData);
		}

		view.OnNewPlayerSignUp += NewPlayer;
		view.OnPlayButtonClicked += Play;
	}

	private void NewPlayer(string name)
	{
		PlayerData playerData = new()
		{
			PlayerName = name,
			LevelProgression = 0,
			Score = 0
		};
		
		playerStorage.SetPlayerData(playerData);
		UpdateView(playerData);
	}

	private void Play()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	private void UpdateView(PlayerData playerData)
	{
		view.SetLevelTxt(playerData.LevelProgression + 1);
		view.SetPlayerNameTxt(playerData.PlayerName);
	}

	public void Dispose()
	{
		
	}
}