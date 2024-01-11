using UnityEngine;

namespace Services.Input
{
    public class InputService : IService
    {
        public Vector2 MoveAxis =>
            new(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));

        public bool IsAttackPress { get; private set; }
        public bool IsSuperAttackPress { get; private set; }

        public void SetAttackPress(bool isPress) => 
            IsAttackPress = isPress;

        public void SetSuperAttackPress(bool isPress) => 
            IsSuperAttackPress = isPress;
    }
}