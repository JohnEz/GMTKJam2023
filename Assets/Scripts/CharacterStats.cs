using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public string Name = "Unknown";

    public float MoveSpeed;

    [SerializeField]
    private List<int> _healthBars = new() { 100 };

    private int _currentHealthBar = 0;

    public int MaxHealth => _healthBars[_currentHealthBar];

    private int _currentHealth;

    [HideInInspector]
    public int CurrentHealth { get => _currentHealth; set => SetCurrentHealth(value); }

    public bool IsDead { get; private set; }

    public Action OnHealthChanged;

    public Action<int> OnHealthBarDepleted;

    public Action OnDeath;

    private void Awake() {
        _currentHealthBar = 0;
        CurrentHealth = MaxHealth;
    }

    private void SetCurrentHealth(int value) {
        _currentHealth = Math.Clamp(value, 0, IsDead ? 0 : MaxHealth);

        if (!IsDead && _currentHealth <= 0) {
            OnHealthBarDepleted?.Invoke(_currentHealthBar);

            int nextHealthBar = _currentHealthBar + 1;

            if (nextHealthBar < _healthBars.Count) {
                _currentHealthBar = nextHealthBar;
                _currentHealth = _healthBars[_currentHealthBar];
            } else {
                IsDead = true;
                OnDeath?.Invoke();
            }
        }

        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage) {
        CurrentHealth -= damage;
    }

    public void TakeHealing(int healing) {
        CurrentHealth += healing;
    }
}
