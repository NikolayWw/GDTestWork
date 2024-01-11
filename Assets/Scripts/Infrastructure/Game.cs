using Infrastructure.Logic;
using Infrastructure.States;
using UnityEngine;
using static StaticData.GameConstants;

namespace Infrastructure
{
    public class Game : MonoBehaviour, ICoroutineRunner
    {
        private const string InitialSceneKey = "Initial";

        public void StartGame()
        {
            DontDestroyOnLoad(this);

            SceneLoader sceneLoader = new(this);
            sceneLoader.Load(InitialSceneKey, () =>
             {
                 Services.RegisterServices services = new(sceneLoader);
                 services.Container.Single<GameStateMachine>().Enter<LoadLevelState, string>(GameSceneKey);
             });
        }
    }
}