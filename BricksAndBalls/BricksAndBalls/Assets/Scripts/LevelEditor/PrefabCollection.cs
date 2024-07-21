using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabCollection", menuName = "LevelEditor/PrefabCollection")]
public class PrefabCollection : ScriptableObject
{
	public List<PrefabItem> PrefabItems = new List<PrefabItem>();
}
