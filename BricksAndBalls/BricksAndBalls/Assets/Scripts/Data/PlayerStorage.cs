using System.IO;
using UnityEngine;

public class PlayerStorage : IPlayerStorage
{
	private readonly string filePath;

	public PlayerStorage(string filePath)
	{
		this.filePath = string.IsNullOrEmpty(filePath) 
			? Path.Combine(Application.persistentDataPath, "playerData.json") : filePath;
	}

	public void SetPlayerData(PlayerData playerData)
	{
		string data = JsonUtility.ToJson(playerData);
		File.WriteAllText(filePath, data);
	}

	public PlayerData GetPlayerData()
	{
		if (File.Exists(filePath))
		{
			string data = File.ReadAllText(filePath);
			return JsonUtility.FromJson<PlayerData>(data);
		}

		return new PlayerData();
	}
}