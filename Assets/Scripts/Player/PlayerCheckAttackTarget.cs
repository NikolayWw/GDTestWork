using Logic.TakeDamage;
using StaticData.Player;
using UnityEngine;

namespace Player
{
    public class PlayerCheckAttackTarget : MonoBehaviour
    {
        private PlayerStaticData _config;
        private readonly Collider[] _attackColliders = new Collider[10];

        [SerializeField] private Transform _hitPoint;
        public readonly ITakeDamage[] Targets = new ITakeDamage[1];
        public bool TargetPresent { get; private set; }

        public void Construct(PlayerStaticData config)
        {
            _config = config;
        }

        private void FixedUpdate()
        {
            FindTargets();
        }

        private void FindTargets()
        {
            Targets[0] = null;

            int count = Physics.OverlapSphereNonAlloc(_hitPoint.position, _config.AttackRadius, _attackColliders, _config.AttackLayerMask, QueryTriggerInteraction.Ignore);
            for (var i = 0; i < count; i++)
            {
                Rigidbody attachedRigidbody = _attackColliders[i].attachedRigidbody;

                if (attachedRigidbody != null
                    && attachedRigidbody.TryGetComponent(out ITakeDamage takeDamage)
                    && takeDamage.Happened == false)
                {
                    Targets[0] = takeDamage;
                    TargetPresent = true;
                    return;
                }
            }
            TargetPresent = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(_hitPoint.position, 0.2f);

            if (_config != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(_hitPoint.position, _config.AttackRadius);
            }
        }
    }
}