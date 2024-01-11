using UnityEngine;

namespace StaticData.Player
{
    [CreateAssetMenu(menuName = "Data/Player Static Data", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; } = 10;
        [field: SerializeField] public float Speed { get; private set; } = 5;

        [field: SerializeField] public float AttackRadius { get; private set; } = 2;
        [field: SerializeField] public float Damage { get; private set; } = 1;
        [field: SerializeField] public float SuperDamage { get; private set; } = 2;
        
        [field: SerializeField] public float AttackDelay { get; private set; } = 1;
        [field: SerializeField] public float SuperAttackDelay { get; private set; } = 2;
        [field: SerializeField] public float DelayBetweenAttacks { get; private set; } = 0.6f;
        [field: SerializeField] public LayerMask AttackLayerMask { get; private set; }

        [field: SerializeField] public float HealForKill { get; private set; } = 5;
    }
}