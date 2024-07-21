using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
	[SerializeField] private ScoreView scoreView;
	[SerializeField] private BallLauncher ballLauncher;
	[SerializeField] private BoardManagerEditor boardManagerEditor;
	[SerializeField] private CameraSettings cameraSettings;
	protected override void Configure(IContainerBuilder builder)
	{
		builder.RegisterEntryPoint<GameManager>();
		builder.Register<ScorePresenter>(Lifetime.Singleton);
		builder.Register<GamePlayEvents>(Lifetime.Singleton);
	

		builder.RegisterComponent(boardManagerEditor).As<IBoardManager>();
		builder.RegisterComponent(scoreView);
		builder.RegisterComponent(ballLauncher);
		builder.RegisterComponent(cameraSettings).As<ICameraSettings>();
	}
}