using R3;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainPresenter : IDisposable
{
	readonly IStateContext stateContext;
	readonly CompositeDisposable disposables = new CompositeDisposable();
	readonly DialogsModel dialogsModel;
	readonly MainView mainView;

	public MainPresenter(DialogsModel dialogsModel, MainView mainView, IStateContext stateContext)
	{
		this.dialogsModel = dialogsModel;
		this.mainView = mainView;
		this.stateContext = stateContext;
	}

	public void Initialize()
	{
		mainView.Show();
		OnEnterState();
	}

	public void OnEnterState()
	{
		mainView.SetStateButtonInteractable(false);

		dialogsModel.StartDialog(DialogsKey.Main_Talk_A);

		mainView.DisableClick();
		ShowNextLine();

		mainView.OnClicked += ShowNextLine;
		mainView.OnStateButtonClicked += OnStateButtonClicked;

		mainView.EnableClick();
	}

	/// <summary>
	/// ï∂èÕíPà 
	/// </summary>
	public void ShowNextLine()
	{
		if (!dialogsModel.HasNextLine())
		{
			// ì¸óÕí‚é~
			mainView.OnClicked -= ShowNextLine;
			mainView.EnableClick();

			mainView.SetStateButtonInteractable(true);
			return;
		}

		var line = dialogsModel.NextLine();
		mainView.ShowLine(line.speaker, line.line);
	}

	public void Dispose()
	{
		disposables.Dispose();
		mainView.Hide();
	}

	public void OnStateButtonClicked()
	{
		stateContext.ChangeState<NovelState>(ScopeType.Novel);
		mainView.Hide();
	}

}
