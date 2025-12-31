using UnityEngine;
using VContainer.Unity;

public class GameManager : IStartable
{
	readonly StateMachine stateMachine;

	public GameManager(StateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
	}

	public void Start()
	{
		stateMachine.ChangeState<MainState>(ScopeType.Main);
	}
}
