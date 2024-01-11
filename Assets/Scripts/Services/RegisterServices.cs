using Infrastructure.Logic;
using Infrastructure.States;
using Services.EnemySpawner;
using Services.Factory;
using Services.Input;
using Services.StaticData;
using UI.Services;

namespace Services
{
    public class RegisterServices
    {
        public AllServices Container { get; private set; }

        public RegisterServices(SceneLoader sceneLoader)
        {
            AllServices container = AllServices.Container;
            container.RegisterSingle(new StaticDataService());
            container.RegisterSingle(new GameFactory(container));
            container.RegisterSingle(new UIFactory(container));
            container.RegisterSingle(new InputService());
            container.RegisterSingle(new EnemySpawnerService(container.Single<UIFactory>(), container.Single<GameFactory>()));

            container.RegisterSingle(new GameStateMachine(sceneLoader, container));
            Container = container;
        }
    }
}