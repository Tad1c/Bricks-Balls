using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
	[SerializeField] private ScoreView scoreView;
	[SerializeField] private GameUiView gameUiView;
	[SerializeField] private BallLauncher ballLauncher;
	[SerializeField] private BoardManagerEditor boardManagerEditor;
	[SerializeField] private CameraSettings cameraSettings;
	[SerializeField] private ProgressionDataSo progressionDataSo;
	protected override void Configure(IContainerBuilder builder)
	{
		builder.RegisterEntryPoint<GameManager>();
		builder.Register<ScorePresenter>(Lifetime.Singleton);
		builder.Register<GamePlayEvents>(Lifetime.Singleton);
		builder.Register<GameUiPresenter>(Lifetime.Singleton);
		builder.Register<IPlayerStorage, PlayerStorage>(Lifetime.Singleton).WithParameter("");


		builder.RegisterComponent(progressionDataSo);
		builder.RegisterComponent(boardManagerEditor).As<IBoardManager>();
		builder.RegisterComponent(scoreView);
		builder.RegisterComponent(gameUiView);
		builder.RegisterComponent(ballLauncher);
		builder.RegisterComponent(cameraSettings).As<ICameraSettings>();
	}
}