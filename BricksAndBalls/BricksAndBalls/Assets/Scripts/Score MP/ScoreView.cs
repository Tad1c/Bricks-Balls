using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreTxt;
	public void UpdateScore(int score)
	{
		scoreTxt.SetText($"{score}");
	}
}