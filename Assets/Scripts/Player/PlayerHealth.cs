using Logic.Animation;
using Logic.TakeDamage;
using Services.StaticData;
using StaticData.Player;
using System;
using UI.Services;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, ITakeDamage
    {
        private PlayerStaticData _config;
        private UIFactory _uiFactory;

        [SerializeField] private AnimationHandler _animation;
        public bool Happened { get; private set; }
        public Action OnHappened;
        private float _currentHealth;

        private float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                if (_currentHealth < 0)
                    _currentHealth = 0;
                Debug.Log($"HP: {_currentHealth}");
            }
        }

        public void Construct(UIFactory uiFactory, StaticDataService dataService)
        {
            _uiFactory = uiFactory;
            _config = dataService.PlayerStaticData;
            CurrentHealth = _config.Health;
        }

        public bool TakeDamage(float value)
        {
            if (Happened)
                return true;

            CurrentHealth -= value;
            if (CurrentHealth <= 0)
            {
                Happened = true;
                OnHappened?.Invoke();
                _animation.PlayerHappened();
                _uiFactory.CreateLoseWindow();
                return true;
            }
            return false;
        }

        public void Heal(float value)
        {
            if (Happened)
                return;

            CurrentHealth += value;
        }
    }
}