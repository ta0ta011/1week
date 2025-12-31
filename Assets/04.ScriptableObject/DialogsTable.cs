using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Novel/NovelTable")]
public class DialogsTable : ScriptableObject
{
	int currentIndex = 0;

	[System.Serializable]
	public class Entry
	{
		public DialogsKey key;
		public DialogsData data;
	}

	public Entry[] entries;

	Dictionary<DialogsKey, DialogsData> dict;

	private void OnEnable()
	{
		dict = new Dictionary<DialogsKey, DialogsData>();
		foreach (var e in entries)
		{
			dict[e.key] = e.data;
		}
	}

	public DialogsData Get(DialogsKey key)
	{
		return dict[key];
	}
}
