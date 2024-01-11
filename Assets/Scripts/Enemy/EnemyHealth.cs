using Logic.Animation;
using Logic.TakeDamage;
using StaticData.Enemy;
using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, ITakeDamage
    {
        private EnemyConfig _config;

        [SerializeField] private AnimationHandler _animation;
        [SerializeField] private Collider[] _disableColliders;
        public bool Happened { get; private set; }
        public Action OnHappened;
        private float _currentHealth;

        public void Construct(EnemyConfig config)
        {
            _config = config;
            _currentHealth = _config.Health;
        }

        public bool TakeDamage(float value)
        {
            if (Happened)
                return true;

            _currentHealth -= value;
            if (_currentHealth <= 0f)
            {
                _currentHealth = 0;
                Happened = true;
                OnHappened?.Invoke();
                _animation.PlayerHappened();
                
                foreach (Collider disableCollider in _disableColliders)
                    disableCollider.enabled = false;

                return true;
            }

            return false;
        }
    }
}