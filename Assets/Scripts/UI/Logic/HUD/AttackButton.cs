using Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Logic.HUD
{
    public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private InputService _inputService;

        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputService.SetAttackPress(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputService.SetAttackPress(false);
        }
    }
}