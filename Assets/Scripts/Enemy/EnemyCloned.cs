using Logic.TakeDamage;
using Services.Factory;
using StaticData.Enemy;
using UnityEngine;

namespace Enemy
{
    public class EnemyCloned : MonoBehaviour
    {
        private EnemyConfig _config;
        private GameFactory _gameFactory;
        private ITakeDamage _targetTakeDamage;
        private Transform _targetTransform;

        [SerializeField] private EnemyHealth _health;

        public void Construct(GameFactory gameFactory, EnemyConfig config, ITakeDamage targetTakeDamage, Transform targetTransform)
        {
            _config = config;
            _gameFactory = gameFactory;
            _targetTakeDamage = targetTakeDamage;
            _targetTransform = targetTransform;
            _health.OnHappened += Clone;
        }

        private void Clone()
        {
            for (int i = 0; i < _config.CloneCount; i++)
            {
                Vector3 position = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                _gameFactory.CreateEnemy(_config.CloneId, position, _targetTakeDamage, _targetTransform);
            }
        }
    }
}