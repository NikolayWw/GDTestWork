using UnityEngine;

namespace Logic.Animation
{
    public class AnimationHandler : MonoBehaviour
    {
        private readonly int MoveIdleHash = Animator.StringToHash("MoveIdleBlend");
        private readonly int AttackHash = Animator.StringToHash("sword attack");
        private readonly int SuperAttackHash = Animator.StringToHash("sword double attack");
        private readonly int DieHash = Animator.StringToHash("Die");
        private const int AttackLayer = 1;
        private const int IdleValue = 0;
        private const int WalkingValue = 1;

        [SerializeField] private Animator _animator;

        public void UpdateIdle()
        {
            _animator.SetFloat(MoveIdleHash, IdleValue, 0.15f, Time.deltaTime);
        }

        public void UpdateWalking()
        {
            _animator.SetFloat(MoveIdleHash, WalkingValue);
        }

        public void PlayAttack()
        {
            float currentMove = _animator.GetFloat(MoveIdleHash);
            int layer = Mathf.Approximately(currentMove, WalkingValue) ? AttackLayer : 0;
            _animator.Play(AttackHash, layer, 0.1f);
        }

        public void PlayerHappened()
        {
            _animator.Play(DieHash);
        }

        public void PlaySupperAttack()
        {
            float currentMove = _animator.GetFloat(MoveIdleHash);
            int layer = Mathf.Approximately(currentMove, WalkingValue) ? AttackLayer : 0;
            _animator.Play(SuperAttackHash, layer, 0.1f);
        }
    }
}