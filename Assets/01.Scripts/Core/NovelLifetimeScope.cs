using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class NovelLifetimeScope : LifetimeScope
{
    [SerializeField] NovelView novelView;
	protected override void Configure(IContainerBuilder builder)
    {
		builder.RegisterInstance(novelView);

		builder.Register<DialogsModel>(Lifetime.Scoped);
		//builder.Register<ScenarioModel>(Lifetime.Scoped);
		builder.Register<NovelPresenter>(Lifetime.Scoped);
		builder.Register<NovelState>(Lifetime.Scoped);

	}
}
