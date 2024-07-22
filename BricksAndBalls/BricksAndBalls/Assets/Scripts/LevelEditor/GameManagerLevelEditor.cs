using System;
using VContainer.Unity;

public class GameManagerLevelEditor : IDisposable, IStartable
{
	private readonly ScorePresenter scorePresenter;
	private readonly IBoardManager boardManager;
	
	private GamePlayEvents gamePlayEvent;

	public GameManagerLevelEditor(ScorePresenter scorePresenter,
		GamePlayEvents gamePlayEvent,
		IBoardManager boardManager)
	{
		this.scorePresenter = scorePresenter;
		this.gamePlayEvent = gamePlayEvent;
		this.boardManager = boardManager;

	}

	public void Start()
	{
		gamePlayEvent.OnBrickHit += OnBricksHit;
		gamePlayEvent.OnRoundEnd += RoundEnd;
	}

	private void RoundEnd()
	{
		boardManager.MoveDownBricks();
	}
	
	private void OnBricksHit()
	{
		scorePresenter.UpdateScore();
	}

	public void Dispose()
	{
		gamePlayEvent.OnRoundEnd -= RoundEnd;
		gamePlayEvent.OnBrickHit -= OnBricksHit;
	}
}
