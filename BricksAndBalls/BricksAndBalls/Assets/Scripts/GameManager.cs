using VContainer.Unity;

public class GameManager : IStartable
{
	private readonly BallLauncher ballLauncher;
	private readonly ScorePresenter scorePresenter;
	private readonly GamePlayEvents gamePlayEvent;
	private readonly IBoardManager boardManager;
	
	public GameManager(ScorePresenter scorePresenter, 
		BallLauncher ballLauncher,
		GamePlayEvents gamePlayEvent,
		IBoardManager boardManager)
	{
		this.scorePresenter = scorePresenter;
		this.ballLauncher = ballLauncher;
		this.gamePlayEvent = gamePlayEvent;
		this.boardManager = boardManager;
	}
	
	public void Start()
	{
		gamePlayEvent.OnBrickHit += OnBricksHit;
		boardManager.Test();
	}

	private void OnBricksHit()
	{
		scorePresenter.UpdateScore(1);
	}
}
