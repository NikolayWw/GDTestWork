using Logic.Animation;
using Services.Input;
using Services.StaticData;
using StaticData.Player;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        private InputService _inputService;
        private PlayerStaticData _config;

        [SerializeField] private PlayerHealth _health;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private AnimationHandler _animationHandler;

        public void Construct(StaticDataService dataService, InputService inputService)
        {
            _config = dataService.PlayerStaticData;
            _inputService = inputService;
            _health.OnHappened += DisableThis;
        }

        private void Update()
        {
            UpdateMove();
        }

        private void UpdateMove()
        {
            Vector2 axis = _inputService.MoveAxis;
            if (axis != Vector2.zero)
            {
                float angle = Mathf.Atan2(axis.x, axis.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, angle, 0);

                Vector3 direction = _config.Speed * Time.deltaTime * transform.forward;
                direction.y -= 9.8f * Time.deltaTime;//gravity
                _characterController.Move(direction);

                _animationHandler.UpdateWalking();
            }
            else
            {
                _animationHandler.UpdateIdle();
            }
        }

        private void DisableThis()
        {
            enabled = false;
        }
    }
}