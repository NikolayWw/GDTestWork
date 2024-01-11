using System;
using UnityEngine;

namespace StaticData.Enemy
{
    [Serializable]
    public class EnemyConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public EnemyId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; } = 2;
        [field: SerializeField] public float Speed { get; private set; } = 1;

        [field: SerializeField] public float AttackDistance { get; private set; } = 2;
        [field: SerializeField] public float AttackDelay { get; private set; } = 1;
        [field: SerializeField] public float Damage { get; private set; } = 1;

        [field: SerializeField] public EnemyId CloneId { get; private set; }
        [field: SerializeField] public int CloneCount { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}