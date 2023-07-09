using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public string Name = "Unknown";

    public float MoveSpeed;

    [SerializeField]
    private int _maxHealth = 100;

    [HideInInspector]
    public int MaxHealth { get => _maxHealth; set => SetMaxHealth(value); }

    private int _currentHealth;

    [SerializeField]
    private int _numHealthBars = 1;

    private int _remainingHealthBars;

    [HideInInspector]
    public int CurrentHealth { get => _currentHealth; set => SetCurrentHealth(value); }

    private bool _isDead;

    public bool IsDead { get => _isDead; }

    public Action OnHealthChanged;

    public Action<int> OnHealthBarEmpty;

    public Action OnDeath;

    private void Awake() {
        _remainingHealthBars = _numHealthBars;
        CurrentHealth = MaxHealth;
    }

    private void SetCurrentHealth(int value) {
        _currentHealth = Math.Clamp(value, 0, _isDead ? 0 : _maxHealth);

        if (!_isDead && _currentHealth <= 0) {
            _remainingHealthBars--;

            OnHealthBarEmpty?.Invoke(_remainingHealthBars);

            if (_remainingHealthBars > 0) {
                _currentHealth = _maxHealth;
            } else {
                _isDead = true;
                OnDeath?.Invoke();
            }
        }

        OnHealthChanged?.Invoke();
    }

    private void SetMaxHealth(int value) {
        _maxHealth = value;

        if (_maxHealth < _currentHealth) {
            _currentHealth = _maxHealth;
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
