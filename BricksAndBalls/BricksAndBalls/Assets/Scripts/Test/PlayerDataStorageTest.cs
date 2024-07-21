using System.IO;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class PlayerDataStorageTest
{
	private string testFilePath;
	private IPlayerStorage playerStorage;

	[SetUp]
	public void SetUp()
	{
		testFilePath = Path.Combine(Application.persistentDataPath, "testPlayerData.json");
		playerStorage = new PlayerStorage(testFilePath);
	}

	[TearDown]
	public void TearDown()
	{
		if (File.Exists(testFilePath))
		{
			File.Delete(testFilePath);
		}
	}

	[Test]
	public void Test_SetPlayerData_CreatesFile()
	{
		PlayerData playerData = BuildPlayer();

		playerStorage.SetPlayerData(playerData);

		Assert.IsTrue(File.Exists(testFilePath), "Player data file was not created.");
	}

	[Test]
	public void Test_SetPlayerData_WritesCorrectData()
	{
		PlayerData playerData = BuildPlayer();

		playerStorage.SetPlayerData(playerData);
		string savedData = File.ReadAllText(testFilePath);
		PlayerData loadedData = JsonUtility.FromJson<PlayerData>(savedData);

		Assert.AreEqual(playerData.Score, loadedData.Score, "Player score was not saved correctly.");
		Assert.AreEqual(playerData.LevelProgression, loadedData.LevelProgression,
			"Player level progression was not saved correctly.");
	}

	[Test]
	public void Test_GetPlayerData_ReturnsCorrectData()
	{
		PlayerData playerData = BuildPlayer();
		string data = JsonUtility.ToJson(playerData);
		File.WriteAllText(testFilePath, data);
		
		PlayerData loadedData = playerStorage.GetPlayerData();
		
		Assert.AreEqual(playerData.Score, loadedData.Score, "Player score was not loaded correctly.");
		Assert.AreEqual(playerData.LevelProgression, loadedData.LevelProgression,
			"Player level progression was not loaded correctly.");
	}

	[Test]
	public void Test_GetPlayerData_ReturnsDefaultWhenFileNotExists()
	{
		PlayerData loadedData = playerStorage.GetPlayerData();
		
		Assert.AreEqual(0, loadedData.Score, "Default player score should be 0.");
		Assert.AreEqual(0, loadedData.LevelProgression, "Default player level progression should be 0.");
	}

	private PlayerData BuildPlayer()
	{
		return new PlayerData()
		{
			Score = 100,
			LevelProgression = 1
		};
	}
}