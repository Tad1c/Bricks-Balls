using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnterNamePopupView : MonoBehaviour
{
	[SerializeField] private TMP_InputField nameInputField;
	[SerializeField] private Button createPlayerBtn;

	public event Action<string> OnCreatePlayerBtnClicked;

	private string playerName;

	public void Start()
	{
		createPlayerBtn.onClick.AddListener(() => OnCreatePlayerBtnClicked?.Invoke(playerName));
	}

	public void InputPlayerName(string playerName)
	{
		this.playerName = playerName;
	}
}
