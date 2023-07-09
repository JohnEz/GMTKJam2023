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

    [HideInInspector]
    public int CurrentHealth { get => _currentHealth; set => SetCurrentHealth(value); }

    private bool _isDead;

    public bool IsDead { get => _isDead; }

    public Action OnHealthChanged;

    public Action OnDeath;

    private void Awake() {
        CurrentHealth = MaxHealth;
    }

    private void SetCurrentHealth(int value) {
        _currentHealth = Math.Clamp(value, 0, _isDead ? 0 : _maxHealth);

        if (!_isDead && _currentHealth <= 0) {
            _isDead = true;
            OnDeath?.Invoke();
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
