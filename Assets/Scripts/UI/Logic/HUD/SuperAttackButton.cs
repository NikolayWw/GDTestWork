using Player;
using Services.Factory;
using Services.Input;
using StaticData.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Logic.HUD
{
    public class SuperAttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private PlayerCheckAttackTarget _checkAttackTarget;
        private PlayerStaticData _config;
        private InputService _inputService;
        private PlayerAttack _attack;

        [SerializeField] private Image _cooldownImage;
        [SerializeField] private Button _superAttackButton;

        public void Construct(PlayerStaticData config, GameFactory gameFactory, InputService inputService)
        {
            _config = config;
            _checkAttackTarget = gameFactory.Player.GetComponent<PlayerCheckAttackTarget>();
            _attack = gameFactory.Player.GetComponent<PlayerAttack>();
            _inputService = inputService;
        }

        private void Update()
        {
            UpdateInteractableButton();
            UpdateCooldownIndicator();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_superAttackButton.interactable)
                _inputService.SetSuperAttackPress(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputService.SetSuperAttackPress(false);
        }

        private void UpdateInteractableButton()
        {
            bool interactable = _checkAttackTarget.TargetPresent && _attack.SuperAttackCurrentTime >= _config.SuperAttackDelay;
            _superAttackButton.interactable = interactable;
        }

        private void UpdateCooldownIndicator()
        {
            _cooldownImage.fillAmount = _attack.SuperAttackCurrentTime / _config.SuperAttackDelay;
        }
    }
}