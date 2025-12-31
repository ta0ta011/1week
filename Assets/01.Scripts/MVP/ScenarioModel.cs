using UnityEngine;

//　最後に実行された選択肢を管理する　
public class ScenarioModel 
{
	public ChoiceType LastChoice { get; private set; }
	public bool IsFirstEntry { get; private set; } = true;

	public void SetLastChoice(ChoiceType choice)
	{
		LastChoice = choice;
		IsFirstEntry = false; // 選択肢を選んだら初回ではなくなる

	}
}
