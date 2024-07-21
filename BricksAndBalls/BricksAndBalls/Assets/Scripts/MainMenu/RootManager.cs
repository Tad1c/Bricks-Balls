using VContainer.Unity;

public class RootManager : IStartable
{
	private readonly IPlayerStorage playerStorage;

	public RootManager(IPlayerStorage playerStorage)
	{
		this.playerStorage = playerStorage;
	}
	
	public void Start()
	{
		PlayerData playerData = playerStorage.GetPlayerData();
		if (string.IsNullOrEmpty(playerData.PlayerName))
		{
			
		}
	}
}
