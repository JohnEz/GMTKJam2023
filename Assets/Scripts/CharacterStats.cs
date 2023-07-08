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

    public Action OnHealthChanged;

    private void Awake() {
        CurrentHealth = MaxHealth;
    }

    private void SetCurrentHealth(int value) {
        _currentHealth = Math.Clamp(value, 0, _maxHealth);

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
}
