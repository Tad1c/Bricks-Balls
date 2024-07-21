using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProgressionData", menuName = "ProgressionData")]
public class ProgressionDataSo : ScriptableObject
{
	[SerializeField] private List<LevelData> levels;

	public LevelData GetLevel(int lvl)
	{
		return levels[lvl];
	}
}
