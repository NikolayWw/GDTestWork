using Logic.Animation;
using Logic.TakeDamage;
using StaticData.Enemy;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private ITakeDamage _targetTakeDamage;
        private EnemyConfig _config;
        private Transform _targetTransform;

        [SerializeField] private AnimationStateReporter _animationReporter;
        [SerializeField] private AnimationHandler _animation;
        [SerializeField] private EnemyHealth _health;

        private float _currentDelay;

        public void Construct(EnemyConfig config, ITakeDamage targetTakeDamage, Transform targetTransform)
        {
            _config = config;
            _targetTakeDamage = targetTakeDamage;
            _targetTransform = targetTransform;
            _health.OnHappened += DisableThis;
            _animationReporter.OnReport += SetDamage;
        }

        private void Update()
        {
            _currentDelay += Time.deltaTime;
            UpdateTryAttack();
        }

        private void UpdateTryAttack()
        {
            if (ConditionAttack() == false)
                return;

            _currentDelay = 0;
            _animation.PlayAttack();
        }

        private bool ConditionAttack()
        {
            if (Vector3.Distance(transform.position, _targetTransform.position) > _config.AttackDistance)
                return false;

            if (_currentDelay < _config.AttackDelay)
                return false;
            return true;
        }

        private void SetDamage(AnimationStateId id)
        {
            if (AnimationStateId.Attack != id)
                return;

            _targetTakeDamage.TakeDamage(_config.Damage);
        }

        private void DisableThis()
        {
            enabled = false;
        }
    }
}