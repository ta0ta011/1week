using UnityEngine;

[CreateAssetMenu(fileName = "NewNovel", menuName = "NovelData")]
public class DialogsData : ScriptableObject
{
	public DialogsLine[] novelDataLines;
}

[System.Serializable]
public class DialogsLine
{
	public Speaker speaker;
	[TextArea]
	public string line;
}

[System.Serializable]
public class Choice
{
	public string choiceText;
	public DialogsKey nextDialog; //‘I‚ñ‚Å	
}