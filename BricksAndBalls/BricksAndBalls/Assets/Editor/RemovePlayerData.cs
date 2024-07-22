using System.IO;
using UnityEditor;
using UnityEngine;

public class RemovePlayerData
{

	[MenuItem("Tools/Clear Data")]
	public static void ClearData()
	{
		string path = Path.Combine(Application.persistentDataPath, "playerData.json");
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}
	
}
