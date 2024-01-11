using Logic.Animation;
using StaticData.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        private EnemyConfig _config;
        private Transform _targetTransform;

        [SerializeField] private AnimationHandler _animation;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyHealth _health;

        public void Construct(EnemyConfig enemyConfig, Transform targetTransform)
        {
            _config = enemyConfig;
            _targetTransform = targetTransform;
            _agent.speed = _config.Speed;
            _health.OnHappened += DisableThis;
            _agent.stoppingDistance = _config.AttackDistance - 0.2f;//offset for attack
        }

        private void Update()
        {
            _agent.SetDestination(_targetTransform.position);
            UpdateRotate();
            UpdateAnimator();
        }

        private void DisableThis()
        {
            enabled = false;
            _agent.SetDestination(transform.position);
            _agent.enabled = false;
        }

        private void UpdateRotate()
        {
            Quaternion lookAtTarget = Quaternion.LookRotation(_targetTransform.position - transform.position, Vector3.up);
            lookAtTarget.x = 0;
            lookAtTarget.z = 0;
            transform.rotation = lookAtTarget;
        }

        private void UpdateAnimator()
        {
            if (_agent.velocity.magnitude > 0.2f) //condition to walking
                _animation.UpdateWalking();
            else
                _animation.UpdateIdle();
        }
    }
}