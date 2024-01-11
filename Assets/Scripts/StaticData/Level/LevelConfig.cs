using UnityEngine;

namespace StaticData.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Data/BattleCampInfo")]
    public class LevelConfig : ScriptableObject
    {
        public Wave[] Waves;

        private void OnValidate()
        {
            for (var i = 0; i < Waves.Length; i++)
                Waves[i].OnValidate($"Wave {i + 1}");
        }
    }
}