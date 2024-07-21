using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabCollection", menuName = "LevelEditor/PrefabCollection")]
public class PrefabCollectionSo : ScriptableObject
{
	public List<PrefabItem> PrefabItems = new List<PrefabItem>();
}
