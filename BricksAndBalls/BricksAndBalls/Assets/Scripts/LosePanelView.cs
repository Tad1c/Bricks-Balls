using System;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelView : MonoBehaviour
{
	[SerializeField] private Button homeButton;
	[SerializeField] private Button retryButton;

	public void SetButtons(Action onHome, Action onRetry)
	{
		homeButton.onClick.AddListener(() => onHome?.Invoke());
		retryButton.onClick.AddListener(() => onRetry?.Invoke());
	}
}
