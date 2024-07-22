using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelEditorLifetimeScope : LifetimeScope
{
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private BallLauncher ballLauncher;
    [SerializeField] private BoardManagerEditor boardManager;
    [SerializeField] private CameraSettings cameraSettings;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameManagerLevelEditor>();
        builder.Register<ScorePresenter>(Lifetime.Singleton);
        builder.Register<GamePlayEvents>(Lifetime.Singleton);
        
        builder.RegisterComponent(boardManager).As<IBoardManager>();
        builder.RegisterComponent(scoreView);
        builder.RegisterComponent(ballLauncher);
        builder.RegisterComponent(cameraSettings).As<ICameraSettings>();
    }
}
