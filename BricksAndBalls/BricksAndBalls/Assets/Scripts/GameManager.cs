using System;
using VContainer.Unity;

public class GameManager : IStartable, IDisposable
{
	private readonly ScorePresenter scorePresenter;
	private readonly IBoardManager boardManager;
	private readonly IPlayerStorage playerStorage;
	private readonly ProgressionDataSo progressionDataSo;
	
	private GamePlayEvents gamePlayEvent;
	
	public GameManager(ScorePresenter scorePresenter, 
		GamePlayEvents gamePlayEvent,
		IBoardManager boardManager,
		IPlayerStorage playerStorage,
		ProgressionDataSo progressionDataSo)
	{
		this.scorePresenter = scorePresenter;
		this.gamePlayEvent = gamePlayEvent;
		this.boardManager = boardManager;
		this.playerStorage = playerStorage;
		this.progressionDataSo = progressionDataSo;
	}
	
	public void Start()
	{
		gamePlayEvent.OnBrickHit += OnBricksHit;
		gamePlayEvent.OnRoundEnd += RoundEnd;
		LevelData levelData = progressionDataSo.GetLevel(playerStorage.GetPlayerData().LevelProgression);
		boardManager.LoadBoard(levelData);
	}


	private void RoundEnd()
	{
		boardManager.MoveDownBricks();
	}
	
	private void OnBricksHit()
	{
		scorePresenter.UpdateScore(1);
	}

	public void Dispose()
	{
		gamePlayEvent.OnRoundEnd -= RoundEnd;
		gamePlayEvent.OnBrickHit -= OnBricksHit;
	}
}
