using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class StateMachine : IStateContext
{
	readonly Dictionary<ScopeType, LifetimeScope> scopeMap;
	readonly IObjectResolver resolver;//Vcontainerの仕組みを利用してStateの生成を行うためのオブジェクトリゾルバ

	private IState currentState;

	LifetimeScope currentScope;

	public StateMachine(
		IObjectResolver resolver,
		MainLifetimeScope mainPrefab,
		NovelLifetimeScope novelPrefab)
	{
		this.resolver = resolver;

		scopeMap = new Dictionary<ScopeType, LifetimeScope>
		{
			{ ScopeType.Main, mainPrefab },
			{  ScopeType.Novel, novelPrefab }
		};
	}

	/// <summary>
	/// ジェネリック型でStateの切り替え
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void ChangeState<T>(ScopeType scopeType) where T : IState
	{
		currentState?.OnExit();
		currentScope?.Dispose();
		currentScope = CreateScope(scopeType);

		if (currentScope == null)
		{
			return;
		}
		// 4. その Scope の Container から State を Resolve
		currentState = currentScope.Container.Resolve<T>();
		// 5. State 開始
		currentState.OnEnter();
	}


	public void UpdateState()
	{
		if (currentState != null)
		{
			currentState.OnUpdate();
		}
	}

	/// <summary>
	/// Scopeの生成（そのシーンでのオブジェクトの期限）
	/// </summary>
	/// <param name="scopeType"></param>
	/// <returns></returns>
	public LifetimeScope CreateScope(ScopeType scopeType)
	{
		if (!scopeMap.ContainsKey(scopeType))
		{
			return null;
		}

		var scopePrefab = scopeMap[scopeType];
		return Object.Instantiate(scopePrefab);
	}
}
