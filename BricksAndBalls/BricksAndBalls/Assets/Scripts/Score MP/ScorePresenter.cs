public class ScorePresenter
{
	private readonly ScoreView view;
	private int score;

	public ScorePresenter(ScoreView view)
	{
		this.view = view;
	}
	
	public void UpdateScore(int point)
	{
		score += point;
		view.UpdateScore(score);
	}
}