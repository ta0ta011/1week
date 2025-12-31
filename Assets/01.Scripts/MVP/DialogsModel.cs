using UnityEngine;

public class DialogsModel 
{
	readonly DialogsTable table;//会話のテーブルデータ

	DialogsData currentData;
	int currentLineIndex;


	public DialogsModel(DialogsTable table)
	{
		this.table = table;
	}

	// 会話開始
	public void StartDialog(DialogsKey key)
	{
		currentData = table.Get(key);
		currentLineIndex = 0;
	}

	/// <summary>
	/// 次の行があるか判定(章的な) 
	/// </summary>
	/// <returns></returns>
	public bool HasNextLine()
	{
		return currentData != null &&
			   currentLineIndex < currentData.novelDataLines.Length;
	}

	/// <summary>
	/// 行を1つ取得する
	/// </summary>
	/// <returns></returns>
	public DialogsLine NextLine()
	{
		if (!HasNextLine())
			return null;

		return currentData.novelDataLines[currentLineIndex++];
	}

	public bool IsDialogFinished()
	{
		return currentData != null &&
			   currentLineIndex >= currentData.novelDataLines.Length;
	}

	public void Clear()
	{
		currentData = null;
		currentLineIndex = 0;
	}

}
