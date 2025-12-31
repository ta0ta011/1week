using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
	[SerializeField] MainLifetimeScope mainScopePrefab;
	[SerializeField] NovelLifetimeScope novelScopePrefab;
	[SerializeField] DialogsTable dialogsTable;

	protected override void Configure(IContainerBuilder builder)
	{
		// エントリーポイント
		builder.RegisterEntryPoint<GameManager>(Lifetime.Singleton);

		// コア
		builder.Register<StateMachine>(Lifetime.Singleton);
		builder.Register<ScenarioModel>(Lifetime.Singleton);
		builder.Register<IStateContext, StateMachine>(Lifetime.Singleton);

		// コンポーネント(MonoBehaviourがあるクラス)
		builder.RegisterInstance(mainScopePrefab);
		builder.RegisterInstance(novelScopePrefab);
		builder.RegisterInstance(dialogsTable);
	}
}
