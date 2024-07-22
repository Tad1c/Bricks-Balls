using TMPro;
using UnityEngine;

public class LeaderBoardEntry : MonoBehaviour
{

	[SerializeField] private TMP_Text nameTxt;
	[SerializeField] private TMP_Text scoreTxt;
	[SerializeField] private RectTransform rectTransform;

	public RectTransform RectTransform => rectTransform;

	public void SetTexts(string name, int score)
	{
		nameTxt.SetText(name);
		scoreTxt.SetText($"{score}");
	}
}
