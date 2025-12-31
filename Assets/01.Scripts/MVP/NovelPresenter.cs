using System;

public class NovelPresenter : IDisposable
{
	enum Phase
	{
		CommonNovel,   // 共通会話
		Choice,        // 選択肢待ち
		BranchNovel    // 分岐会話
	}

	Phase phase;

	readonly DialogsModel novelModel;
	readonly NovelView novelView;
	readonly ScenarioModel scenarioModel;
	readonly IStateContext context;

	//コンストラクタ
	public NovelPresenter(
		IStateContext context,
		DialogsModel novelModel,
		NovelView novelView,
		ScenarioModel scenarioModel
		)
	{
		this.context = context;
		this.novelModel = novelModel;
		this.novelView = novelView;
		this.scenarioModel = scenarioModel;
	}

	//初期化
	public void Initialize()
	{
		novelView.Show();

		//クリックイベント登録
		novelView.OnClickedD += OnDialogClicked;
		novelView.OnChoiceSelected += OnChoiceSelected;

		StartCommonNovel();
	}

	/// <summary>
	/// 共通ノベル部分開始
	/// </summary>
	void StartCommonNovel()
	{
		phase = Phase.CommonNovel;

		novelModel.StartDialog(DialogsKey.Novel_Talk_A);

		novelView.DisableClick();
		ShowNextLine();
		novelView.EnableClick();
	}

	/// <summary>
	/// クリックで次の行へ
	/// </summary>
	void OnDialogClicked()
	{
		if (phase == Phase.CommonNovel || phase == Phase.BranchNovel)
		{
			ShowNextLine();
		}
	}

	/// <summary>
	/// 文章を開始または次の行へ進める
	/// </summary>
	void ShowNextLine()
	{
		if (!novelModel.HasNextLine())
		{
			HandleDialogFinished();
			return;
		}

		var line = novelModel.NextLine();
		novelView.ShowLine(line.speaker, line.line);
	}


	/// <summary>
	/// フェーズの切り替え処理
	/// </summary>
	void HandleDialogFinished()
	{
		if (phase == Phase.CommonNovel)
		{
			OnCommonNovelFinished();
		}
		else if (phase == Phase.BranchNovel)
		{
			OnBranchNovelFinished();
		}
	}

	/// <summary>
	/// 共通ノベル（始めのノベル）⇒　選択肢への切り替え
	/// </summary>
	void OnCommonNovelFinished()
	{
		phase = Phase.Choice;

		novelView.ClearLine();
		novelView.DisableClick();

		novelView.ShowChoices();
		novelView.EnableChoiceButtons();
	}

	/// <summary>
	/// 選択肢開始
	/// </summary>
	/// <param name="choice"></param>
	void OnChoiceSelected(ChoiceType choice)
	{
		if (phase != Phase.Choice)
			return;

		novelView.HideChoices();

		scenarioModel.SetLastChoice(choice);

		var nextKey = choice switch
		{
			ChoiceType.Mix => DialogsKey.Novel_Talk_B,
			ChoiceType.Milk => DialogsKey.Novel_Talk_C,
			ChoiceType.Default => DialogsKey.Novel_Talk_D,
			_ => DialogsKey.None
		};

		StartBranchNovel(nextKey);
	}

	/// <summary>
	/// 分岐の開始	
	/// </summary>
	/// <param name="key"></param>
	void StartBranchNovel(DialogsKey key)
	{
		phase = Phase.BranchNovel;

		novelModel.StartDialog(key);

		novelView.DisableClick();
		ShowNextLine();
		novelView.EnableClick();
	}

	void OnBranchNovelFinished()
	{
		novelView.Hide();
		context.ChangeState<MainState>(ScopeType.Main);
	}

	/// <summary>
	/// 後処理
	/// </summary>
	public void Dispose()
	{
		novelView.OnClickedD -= OnDialogClicked;
		novelView.OnChoiceSelected -= OnChoiceSelected;
	}
}
