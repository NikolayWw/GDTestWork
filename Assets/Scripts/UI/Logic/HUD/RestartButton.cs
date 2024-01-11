using Infrastructure.States;
using StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Logic.HUD
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        public void Construct(GameStateMachine gameStateMachine)
        {
            _restartButton.onClick.AddListener(() => 
                gameStateMachine.Enter<LoadLevelState, string>(GameConstants.GameSceneKey));
        }
    }
}