using UnityEngine.SceneManagement;

public class GameUiPresenter
{
	private readonly GameUiView view;
	private readonly IPlayerStorage playerStorage;
	private readonly ScorePresenter scorePresenter;

	public GameUiPresenter(GameUiView view, 
		IPlayerStorage playerStorage,
		ScorePresenter scorePresenter)
	{
		this.view = view;
		this.playerStorage = playerStorage;
		this.scorePresenter = scorePresenter;

		this.view.OnMultiplierButtonClicked += OnMultiplierButton;
		this.view.OnHomeButtonClicked += OnHomeButtonClicked;
		this.view.OnRetryButtonClicked += OnRetryButtonClicked;
		this.view.OnLeaderboardClicked += ShowLeaderboard;
	}

	public void ShowWinScreen()
	{
		view.ShowWinPanel(scorePresenter.Score, playerStorage.GetPlayerData().Score);
	}

	private void ShowLeaderboard()
	{
		view.ShowLeaderboard();
	}

	public void ShowLoseScreen()
	{
		view.ShowLosePanel();
	}

	private void OnHomeButtonClicked()
	{
		SceneManager.LoadScene("Root", LoadSceneMode.Single);
	}

	private void OnRetryButtonClicked()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	private void OnMultiplierButton(int multiplier)
	{
		int newScore = scorePresenter.Score * multiplier;
		
		PlayerData playerData = playerStorage.GetPlayerData();
		playerData.Score += newScore;
		playerStorage.SetPlayerData(playerData);
		
		view.ShowWinPanel(newScore, playerData.Score);
	}
	
	
}
