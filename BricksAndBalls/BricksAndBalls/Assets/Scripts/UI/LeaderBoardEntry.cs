using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardEntry : MonoBehaviour
{

	[SerializeField] private TMP_Text nameTxt;
	[SerializeField] private TMP_Text scoreTxt;
	[SerializeField] private TMP_Text placeTxt;
	[SerializeField] private RectTransform rectTransform;
	[SerializeField] private Image image;
	[SerializeField] private Color highLightColor;
	[SerializeField] private Color defaultColor;

	public void SetTexts(EntryData entryData, int index)
	{
		nameTxt.SetText(entryData.Name);
		placeTxt.SetText($"#{index}");
		scoreTxt.SetText($"{entryData.Score}");
		if (entryData.IsLocalPlayer)
		{
			image.color = highLightColor;
		}
		else
		{
			image.color = defaultColor;
		}
	}
}

[Serializable]
public struct EntryData
{
	public string Name;
	public int Score;
	public bool IsLocalPlayer;
}
