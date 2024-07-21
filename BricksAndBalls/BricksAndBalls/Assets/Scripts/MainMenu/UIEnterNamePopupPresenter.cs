using System;

public class UIEnterNamePopupPresenter : IDisposable
{
	private readonly UIEnterNamePopupView view;
	private readonly IPlayerStorage playerStorage;

	public UIEnterNamePopupPresenter(UIEnterNamePopupView view, IPlayerStorage playerStorage)
	{
		this.view = view;
		this.playerStorage = playerStorage;
		this.view.OnCreatePlayerBtnClicked += StorePlayerName;
	}

	private void StorePlayerName(string playerName)
	{
		PlayerData playerData = new()
		{
			Score = 0,
			LevelProgression = 0,
			PlayerName = playerName,
		};
		
		playerStorage.SetPlayerData(playerData);
	}

	public void Dispose()
	{
		view.OnCreatePlayerBtnClicked -= StorePlayerName;
	}
}
