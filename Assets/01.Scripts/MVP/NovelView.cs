using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NovelView : MonoBehaviour
{
	[Header("Choice Buttons")]
	[SerializeField] Button milkButton;
	[SerializeField] Button mixButton;
	[SerializeField] Button defaltButton;

	[Header("Text Boxes")]
	[SerializeField] GameObject textBoxMain;
	[SerializeField] GameObject textBoxSub;
	[SerializeField] Text textBoxMainText;
	[SerializeField] Text textBoxSubText;

	// ===== Events =====
	public event Action OnClickedD;
	public event Action<ChoiceType> OnChoiceSelected;

	bool canClickDialog = false;

	void Awake()
	{
		// 選択肢ボタン → Presenterへ通知
		milkButton.onClick.AddListener(() => OnChoiceSelected?.Invoke(ChoiceType.Milk));
		mixButton.onClick.AddListener(() => OnChoiceSelected?.Invoke(ChoiceType.Mix));
		defaltButton.onClick.AddListener(() => OnChoiceSelected?.Invoke(ChoiceType.Default));

		HideChoices();
	}

	void Update()
	{
		if (!canClickDialog)　return;

		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			// UIの上をクリックしてたら無視
			if (EventSystem.current.currentSelectedGameObject != null)　return;
			OnClickedD?.Invoke();
		}
	}

	// ===== 表示制御 =====

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void ShowLine(Speaker speaker, string lineText)
	{
		if (speaker == Speaker.MainCharacter)
		{
			textBoxMain.SetActive(true);
			textBoxSub.SetActive(false);

			textBoxMainText.text = lineText;
			textBoxSubText.text = string.Empty;
		}
		else
		{
			textBoxMain.SetActive(false);
			textBoxSub.SetActive(true);

			textBoxSubText.text = lineText;
			textBoxMainText.text = string.Empty;
		}
	}

	public void ClearLine()
	{
		textBoxMain.SetActive(false);
		textBoxSub.SetActive(false);
	}

	// ===== 会話クリック制御 =====

	public void EnableClick()
	{
		canClickDialog = true;
	}

	public void DisableClick()
	{
		canClickDialog = false;
	}

	// ===== 選択肢制御 =====

	// interactableは、選択できなくなる。
	public void ShowChoices()
	{
		milkButton.gameObject.SetActive(true);
		mixButton.gameObject.SetActive(true);
		defaltButton.gameObject.SetActive(true);
	}

	public void HideChoices()
	{
		milkButton.gameObject.SetActive(false);
		mixButton.gameObject.SetActive(false);
		defaltButton.gameObject.SetActive(false);
	}

	public void EnableChoiceButtons()
	{
		milkButton.interactable = true;
		mixButton.interactable = true;
		defaltButton.interactable = true;
	}

	public void DisableChoiceButtons()
	{
		milkButton.interactable = false;
		mixButton.interactable = false;
		defaltButton.interactable = false;
	}
}
