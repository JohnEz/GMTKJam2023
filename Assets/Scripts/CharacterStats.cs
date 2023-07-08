using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public float MoveSpeed;

    [SerializeField]
    private float _maxHealth;

    [HideInInspector]
    public float MaxHealth { get => _maxHealth; set => SetMaxHealth(value); }

    private float _currentHealth;

    [HideInInspector]
    public float CurrentHealth { get => _currentHealth; set => SetCurrentHealth(value); }

    public Action OnHealthChanged;

    private void Awake() {
    }

    private void SetCurrentHealth(float value) {
        _currentHealth = value;

        OnHealthChanged?.Invoke();
    }

    private void SetMaxHealth(float value) {
        _maxHealth = value;

        OnHealthChanged?.Invoke();
    }
}
