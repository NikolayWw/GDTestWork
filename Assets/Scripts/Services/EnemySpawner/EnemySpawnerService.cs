using Enemy;
using Logic.TakeDamage;
using Services.Factory;
using StaticData.Enemy;
using StaticData.Level;
using System;
using UI.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.EnemySpawner
{
    public class EnemySpawnerService : IService
    {
        private readonly UIFactory _uiFactory;
        private readonly GameFactory _gameFactory;

        public Action OnWaveCountChange;

        public int CurrentWaveIndex { get; private set; }
        private LevelConfig _levelConfig;
        private int _currentEnemyCount;
        private Transform _targetTransform;
        private ITakeDamage _targetTakeDamage;

        public EnemySpawnerService(UIFactory uiFactory, GameFactory gameFactory)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Cleanup()
        {
            OnWaveCountChange = null;
            if (_gameFactory != null)
                _gameFactory.OnEnemyCreated -= SubscribeAndWave;
        }

        public void StartWave(LevelConfig levelConfig, ITakeDamage targetTakeDamage, Transform targetTransform)
        {
            _levelConfig = levelConfig;
            _targetTransform = targetTransform;
            _targetTakeDamage = targetTakeDamage;
            CurrentWaveIndex = 0;
            _currentEnemyCount = 0;
            _gameFactory.OnEnemyCreated += SubscribeAndWave;
            SpawnWave();
        }

        private void SpawnWave()
        {
            Wave configWave = _levelConfig.Waves[CurrentWaveIndex];
            foreach (EnemyId id in configWave.Characters)
            {
                Vector3 pos = new(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                _gameFactory.CreateEnemy(id, pos, _targetTakeDamage, _targetTransform);
            }
        }

        private void EndWaveHandle()
        {
            _currentEnemyCount--;
            if (_currentEnemyCount <= 0)
            {
                CurrentWaveIndex++;
                if (CurrentWaveIndex >= _levelConfig.Waves.Length)
                    _uiFactory.CreateWinWindow();
                else
                {
                    SpawnWave();
                    OnWaveCountChange?.Invoke();
                }
            }
        }

        private void SubscribeAndWave(EnemyHealth health)
        {
            _currentEnemyCount++;
            health.OnHappened += EndWaveHandle;
        }
    }
}