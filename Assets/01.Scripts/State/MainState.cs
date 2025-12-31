using UnityEngine;

public class MainState : IState
{
	readonly IStateContext context;
	readonly MainPresenter mainPresenter;
	readonly ScenarioModel scenarioModel;
	readonly DialogsModel dialogsModel;

	public MainState(
		IStateContext context, 
		MainPresenter mianPresenter,
		ScenarioModel scenarioModel,
		DialogsModel dialogsModel
		)
	{
		this.context = context;
		this.mainPresenter = mianPresenter;
		this.scenarioModel = scenarioModel;
		this.dialogsModel = dialogsModel;
	}

	public void OnEnter()
	{
		mainPresenter.Initialize();

		if (scenarioModel.IsFirstEntry)
		{
			// ゲーム開始時の初期会話
			dialogsModel.StartDialog(DialogsKey.Main_Talk_A);
		}
		else if (scenarioModel.LastChoice != ChoiceType.None)
		{
			// 選択肢に応じた分岐会話
			var key = scenarioModel.LastChoice switch
			{
				ChoiceType.Mix => DialogsKey.Main_Talk_B,
				ChoiceType.Milk => DialogsKey.Main_Talk_C,
				ChoiceType.Default => DialogsKey.Main_Talk_D,
				_ => DialogsKey.None
			};

			if (key != DialogsKey.None)
				dialogsModel.StartDialog(key);
		}
	}

	public void OnUpdate()
	{ 		
	}

	public void OnExit()
	{
		mainPresenter.Dispose();
	}
}
