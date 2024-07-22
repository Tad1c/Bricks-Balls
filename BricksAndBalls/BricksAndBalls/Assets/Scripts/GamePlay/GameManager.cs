using System;
using VContainer.Unity;

public class GameManager : IStartable, IDisposable
{
	private readonly ScorePresenter scorePresenter;
	private readonly IBoardManager boardManager;
	private readonly IPlayerStorage playerStorage;
	private readonly ProgressionDataSo progressionDataSo;
	private readonly GameUiPresenter gameUiPresenter;
	private readonly GamePlayEvents gamePlayEvent;

	public GameManager(ScorePresenter scorePresenter,
		GamePlayEvents gamePlayEvent,
		IBoardManager boardManager,
		IPlayerStorage playerStorage, 
		ProgressionDataSo progressionDataSo,
		GameUiPresenter gameUiPresenter)
	{
		this.scorePresenter = scorePresenter;
		this.gamePlayEvent = gamePlayEvent;
		this.boardManager = boardManager;
		this.playerStorage = playerStorage;
		this.progressionDataSo = progressionDataSo;
		this.gameUiPresenter = gameUiPresenter;
	}

	public void Start()
	{
		gamePlayEvent.OnBrickHit += OnBricksHit;
		gamePlayEvent.OnRoundEnd += RoundEnd;
		gamePlayEvent.OnGameWin += GameWin;
		gamePlayEvent.OnGameLose += GameLose; 
		LevelData levelData = progressionDataSo.GetLevel(playerStorage.GetPlayerData().LevelProgression);
		boardManager.LoadBoard(levelData);
	}

	private void RoundEnd()
	{
		boardManager.MoveDownBricks();
	}

	private void GameWin()
	{
		PlayerData levelData = playerStorage.GetPlayerData();
		levelData.Score += scorePresenter.Score;
		levelData.LevelProgression += 1;
		playerStorage.SetPlayerData(levelData);
		
		gameUiPresenter.ShowWinScreen();
	}

	private void GameLose()
	{
		gameUiPresenter.ShowLoseScreen();
	}

	private void OnBricksHit()
	{
		scorePresenter.UpdateScore();
	}

	public void Dispose()
	{
		gamePlayEvent.OnRoundEnd -= RoundEnd;
		gamePlayEvent.OnBrickHit -= OnBricksHit;
		gamePlayEvent.OnGameWin -= GameWin;
		gamePlayEvent.OnGameLose -= GameLose;
	}
}