using Services.EnemySpawner;
using StaticData.Level;
using TMPro;
using UnityEngine;

namespace UI.Logic.HUD
{
    public class CurrentWaveText : MonoBehaviour
    {
        private EnemySpawnerService _enemySpawnerService;

        [SerializeField] private TMP_Text _waveText;
        private int _maxWavesCount;

        public void Construct(EnemySpawnerService enemySpawnerService, LevelConfig levelConfig)
        {
            _enemySpawnerService = enemySpawnerService;
            _maxWavesCount = levelConfig.Waves.Length;
            _enemySpawnerService.OnWaveCountChange += Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            _waveText.text = $"Wave {_enemySpawnerService.CurrentWaveIndex + 1}/{_maxWavesCount}";
        }
    }
}