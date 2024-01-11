using Infrastructure.Logic;
using Player;
using Services.EnemySpawner;
using Services.Factory;
using Services.StaticData;
using StaticData;
using UI.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly UIFactory _uiFactory;
        private readonly StaticDataService _dataService;
        private readonly GameFactory _factory;
        private readonly EnemySpawnerService _enemySpawner;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, UIFactory uiFactory, StaticDataService dataService, GameFactory factory, EnemySpawnerService enemySpawner)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _dataService = dataService;
            _factory = factory;
            _enemySpawner = enemySpawner;
        }

        public void Enter(string sceneName)
        {
            Clean();
            if (CurrentSceneKey() != sceneName)
                _sceneLoader.Load(sceneName, OnLoaded);
            else
            {
                _sceneLoader.Load(GameConstants.ReloadSceneKey, () =>
                     _sceneLoader.Load(sceneName, OnLoaded));
            }
        }

        public void Exit()
        { }

        private void OnLoaded()
        {
            _factory.CreatePlayer(Vector3.zero);
            InitEnemyWave(_factory.Player);

            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHUD();

            _stateMachine.Enter<LoopState>();
        }

        private void InitEnemyWave(GameObject playerInstance)
        {
            _enemySpawner.StartWave(_dataService.LevelConfig, playerInstance.GetComponent<PlayerHealth>(),
                playerInstance.transform);
        }

        private void Clean()
        {
            _enemySpawner.Cleanup();
            _factory.Cleanup();
        }

        private static string CurrentSceneKey() =>
            SceneManager.GetActiveScene().name;
    }
}