public class ScorePresenter
{
	private const int POINT = 1;
	
	private readonly ScoreView view;
	private int score;

	public int Score => score;
	
	public ScorePresenter(ScoreView view)
	{
		this.view = view;
	}
	
	public void UpdateScore()
	{
		score += POINT;
		view.UpdateScore(score);
	}
}