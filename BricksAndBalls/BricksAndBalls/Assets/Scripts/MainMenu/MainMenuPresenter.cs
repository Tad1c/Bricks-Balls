public class MainMenuPresenter
{
	private readonly MainMenuView view;
	private readonly IPlayerStorage playerStorage;

	public MainMenuPresenter(MainMenuView view, IPlayerStorage playerStorage)
	{
		this.view = view;
		this.playerStorage = playerStorage;
	}

	public void Start()
	{
		PlayerData playerData = playerStorage.GetPlayerData();
		view.SetLevelTxt(playerData.LevelProgression + 1);
		view.SetPlayerNameTxt(playerData.PlayerName);
	}
}