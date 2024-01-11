using Enemy;
using Logic.TakeDamage;
using Player;
using Services.Input;
using Services.StaticData;
using StaticData.Enemy;
using System;
using UI.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.Factory
{
    public class GameFactory : IService
    {
        private readonly AllServices _services;
        public Action<EnemyHealth> OnEnemyCreated;
        public GameObject Player { get; private set; }
        public GameFactory(AllServices services)
        {
            _services = services;
        }

        public void Cleanup()
        {
            OnEnemyCreated = null;
        }

        public void CreatePlayer(Vector3 at)
        {
            StaticDataService staticDataService = GetService<StaticDataService>();
            InputService inputService = GetService<InputService>();
            UIFactory uiFactory = GetService<UIFactory>();
            GameObject prefab = staticDataService.PlayerStaticData.Prefab;

            GameObject instance = Object.Instantiate(prefab, at, Quaternion.identity);
            instance.GetComponent<PlayerMove>().Construct(staticDataService, inputService);
            instance.GetComponent<PlayerAttack>().Construct(staticDataService.PlayerStaticData, inputService);
            instance.GetComponent<PlayerHealth>().Construct(uiFactory, staticDataService);
            instance.GetComponent<PlayerCheckAttackTarget>().Construct(staticDataService.PlayerStaticData);
            Player = instance;
        }

        public void CreateEnemy(EnemyId id, Vector3 at, ITakeDamage targetTakeDamage, Transform targetTransform)
        {
            StaticDataService staticDataService = GetService<StaticDataService>();
            EnemyConfig config = staticDataService.ForEnemy(id);

            GameObject instance = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            instance.GetComponent<EnemyAttack>().Construct(config, targetTakeDamage, targetTransform);
            instance.GetComponent<EnemyMove>().Construct(config, targetTransform);
            instance.GetComponent<EnemyCloned>().Construct(this, config, targetTakeDamage, targetTransform);
            EnemyHealth health = instance.GetComponent<EnemyHealth>();
            health.Construct(config);

            OnEnemyCreated?.Invoke(health);
        }

        private TService GetService<TService>() where TService : IService =>
            _services.Single<TService>();
    }
}