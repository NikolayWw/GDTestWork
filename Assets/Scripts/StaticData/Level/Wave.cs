using StaticData.Enemy;
using System;
using UnityEngine;

namespace StaticData.Level
{
    [Serializable]
    public class Wave
    {
        [SerializeField] private string _inspectorName;
        public EnemyId[] Characters;

        public void OnValidate(string waveName)
        {
            _inspectorName = waveName;
        }
    }
}