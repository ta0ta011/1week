using UnityEngine;

public interface INovelView
{
	event System.Action OnClicked;
	public void ShowLine(Speaker speaker, string lineText);
	public void Hide();
}
