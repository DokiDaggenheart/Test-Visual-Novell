using Zenject;

public class CardGameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CardPooler>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<ICardSpawner>()
            .To<CardSpawner>()
            .AsSingle()
            .NonLazy();

        Container.Bind<GridBinder>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}
