using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
	[SerializeField] MainView mainView;

	protected override void Configure(IContainerBuilder builder)
	{
		builder.RegisterInstance(mainView);

		builder.Register<DialogsModel>(Lifetime.Scoped);
		//builder.Register<ScenarioModel>(Lifetime.Scoped);
		builder.Register<MainPresenter>(Lifetime.Scoped);
		builder.Register<MainState>(Lifetime.Scoped);
	}
}
