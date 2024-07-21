using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{

	[SerializeField] private ProgressionDataSo progressionDataSo;
	[SerializeField] private MainMenuView mainMenuView;
	
	protected override void Configure(IContainerBuilder builder)
	{
		builder.RegisterEntryPoint<MainMenuPresenter>();
		builder.Register<IPlayerStorage, PlayerStorage>(Lifetime.Singleton).WithParameter("");
	
		
		builder.RegisterComponent(progressionDataSo);
		builder.RegisterComponent(mainMenuView);
	}
}