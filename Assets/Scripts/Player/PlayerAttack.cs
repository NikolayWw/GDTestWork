using Logic.Animation;
using Services.Input;
using StaticData.Player;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerStaticData _config;
        private InputService _inputService;

        [SerializeField] private PlayerHealth _health;
        [SerializeField] private AnimationHandler _animationHandler;
        [SerializeField] private AnimationStateReporter _animationReporter;
        [SerializeField] private PlayerCheckAttackTarget _checkTargets;

        public float SuperAttackCurrentTime { get; private set; }
        private float _attackCurrentTime;
        private float _betweenAttacksTime;

        public void Construct(PlayerStaticData config, InputService inputService)
        {
            _config = config;
            _inputService = inputService;
            _attackCurrentTime = _config.AttackDelay;
            SuperAttackCurrentTime = _config.SuperAttackDelay;
            _betweenAttacksTime = _config.DelayBetweenAttacks;

            _animationReporter.OnReport += SetDamage;
        }

        private void Update()
        {
            _attackCurrentTime += Time.deltaTime;
            SuperAttackCurrentTime += Time.deltaTime;
            _betweenAttacksTime += Time.deltaTime;

            if (_health.Happened)
                return;

            UpdateAttack();
            UpdateSuperAttack();
        }

        private void SetDamage(AnimationStateId id)
        {
            switch (id)
            {
                case AnimationStateId.Attack:
                    SetDamage(_config.Damage);
                    break;

                case AnimationStateId.SuperAttack:
                    SetDamage(_config.SuperDamage);
                    break;
            }
        }

        private void UpdateAttack()
        {
            if (_inputService.IsAttackPress == false
                || _attackCurrentTime < _config.AttackDelay
                || _betweenAttacksTime < _config.DelayBetweenAttacks)
                return;

            _attackCurrentTime = 0;
            _betweenAttacksTime = 0;
            _animationHandler.PlayAttack();
        }

        private void UpdateSuperAttack()
        {
            if (_inputService.IsSuperAttackPress == false
                || SuperAttackCurrentTime < _config.SuperAttackDelay
                || _betweenAttacksTime < _config.DelayBetweenAttacks)
                return;

            SuperAttackCurrentTime = 0;
            _betweenAttacksTime = 0;
            _animationHandler.PlaySupperAttack();
        }

        private void SetDamage(float value)
        {
            if (_checkTargets.TargetPresent)
            {
                bool targetDie = _checkTargets.Targets[0].TakeDamage(value);
                if (targetDie)
                    _health.Heal(_config.HealForKill);
            }
        }
    }
}