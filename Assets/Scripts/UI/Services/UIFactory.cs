using Infrastructure.States;
using Services;
using Services.EnemySpawner;
using Services.Factory;
using Services.Input;
using Services.StaticData;
using UI.Logic.HUD;
using UnityEngine;

namespace UI.Services
{
    public class UIFactory : IService
    {
        private readonly AllServices _services;
        private Transform _uiRoot;

        public UIFactory(AllServices services)
        {
            _services = services;
        }

        public void CreateUIRoot()
        {
            StaticDataService staticData = GetService<StaticDataService>();
            GameObject prefab = staticData.WindowStaticData.UiRootPrefab;
            _uiRoot = Object.Instantiate(prefab).transform;
        }

        public void CreateHUD()
        {
            StaticDataService staticData = GetService<StaticDataService>();
            GameStateMachine gameStateMachine = GetService<GameStateMachine>();
            EnemySpawnerService enemySpawnerService = GetService<EnemySpawnerService>();
            GameFactory gameFactory = GetService<GameFactory>();
            InputService inputService = GetService<InputService>();
            GameObject prefab = staticData.WindowStaticData.HUDPrefab;

            GameObject hudInstance = Object.Instantiate(prefab, _uiRoot);
            hudInstance.GetComponentInChildren<RestartButton>().Construct(gameStateMachine);
            hudInstance.GetComponentInChildren<CurrentWaveText>().Construct(enemySpawnerService, staticData.LevelConfig);
            hudInstance.GetComponentInChildren<SuperAttackButton>().Construct(staticData.PlayerStaticData, gameFactory, inputService);
            hudInstance.GetComponentInChildren<AttackButton>().Construct(inputService);
        }

        public void CreateWinWindow()
        {
            StaticDataService staticData = GetService<StaticDataService>();
            GameObject prefab = staticData.WindowStaticData.WinWindowPrefab;
            Object.Instantiate(prefab, _uiRoot);
        }

        public void CreateLoseWindow()
        {
            StaticDataService staticData = GetService<StaticDataService>();
            GameObject prefab = staticData.WindowStaticData.LoseWindowPrefab;
            Object.Instantiate(prefab, _uiRoot);
        }

        private TService GetService<TService>() where TService : IService =>
            _services.Single<TService>();
    }
}