using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using R3;

public class MainView : MonoBehaviour, INovelView
{
	[SerializeField] Button stateButton;
	[SerializeField] GameObject textBoxMain;
	[SerializeField] GameObject textBoxSub;
	[SerializeField] Text textBoxMainText;
	[SerializeField] Text textBoxSubText;

	public event Action OnClicked;
	public event Action OnStateButtonClicked;
	bool canClick = false;

	void Awake()
	{
		stateButton.onClick.AddListener(() =>
		{
			OnStateButtonClicked?.Invoke();
		});
	}

	/// <summary>
	/// テキストクリック検出
	/// </summary>
	public void Update()
	{
		if (!canClick) return;
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			if (EventSystem.current.currentSelectedGameObject != null) return;
			OnClicked?.Invoke();//クリック通知
		}
	}

	public void SetStateButtonInteractable(bool interactable)
	{
		stateButton.interactable = interactable;
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		textBoxMain.SetActive(false);
		textBoxSub.SetActive(false);
	}

	public void ShowLine(Speaker speaker, string lineText)
	{
		if (speaker == Speaker.MainCharacter)
		{
			textBoxMain.SetActive(true);
			textBoxSub.SetActive(false);

			textBoxMainText.text = lineText;
			textBoxSubText.text = string.Empty; // 明示的に消す
		}
		else if (speaker == Speaker.SubCharacter)
		{
			textBoxMain.SetActive(false);
			textBoxSub.SetActive(true);

			textBoxSubText.text = lineText;
			textBoxMainText.text = string.Empty; // 明示的に消す
		}
	}

	public void EnableClick() => canClick = true;
	public void DisableClick() => canClick = false;
}
