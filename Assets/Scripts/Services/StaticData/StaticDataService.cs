using StaticData.Enemy;
using StaticData.Level;
using StaticData.Player;
using StaticData.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IService
    {
        private const string PlayerDataPath = "Player/PlayerStaticData";
        private const string LevelDataPath = "Level/LevelConfig";
        private const string EnemyDataPath = "Enemy/EnemyStaticData";
        private const string WindowDataPath = "UI/WindowStaticData";

        public PlayerStaticData PlayerStaticData { get; private set; }
        public LevelConfig LevelConfig { get; private set; }
        public WindowStaticData WindowStaticData { get; private set; }
        private Dictionary<EnemyId, EnemyConfig> _enemyConfigs;

        public StaticDataService()
        {
            Load();
        }

        private void Load()
        {
            PlayerStaticData = Resources.Load<PlayerStaticData>(PlayerDataPath);
            WindowStaticData = Resources.Load<WindowStaticData>(WindowDataPath);
            LevelConfig = Resources.Load<LevelConfig>(LevelDataPath);
            _enemyConfigs = Resources.Load<EnemyStaticData>(EnemyDataPath).Configs.ToDictionary(x => x.Id, x => x);
        }

        public EnemyConfig ForEnemy(EnemyId id) =>
            _enemyConfigs.TryGetValue(id, out EnemyConfig cfg) ? cfg : null;
    }
}