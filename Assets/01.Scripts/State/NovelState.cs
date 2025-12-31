using UnityEngine;

public class NovelState : IState
{
	readonly NovelPresenter novelPresenter;

	public NovelState(NovelPresenter novelPresenter)
	{
		this.novelPresenter = novelPresenter;
	}

	public void OnEnter()
	{
		novelPresenter.Initialize();
	}
	public void OnUpdate()
	{
	}

	public void OnExit()
	{
		novelPresenter.Dispose();
	}


}
