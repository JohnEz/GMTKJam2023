using System;
using UnityEngine;

public class Damagable : MonoBehaviour {
    private CharacterStats _myStats;

    [SerializeField]
    private DamageFlash _flashTarget;

    private void Awake() {
        _myStats = GetComponent<CharacterStats>();
    }

    public void TakeDamage(int damage, Action onResolve = null) {
        if (_myStats) {
            if (_myStats.IsDead) {
                return;
            }

            _myStats.TakeDamage(damage);
        }

        if (_flashTarget) {
            _flashTarget.StartFlash();
        }

        onResolve?.Invoke();
    }

    public void TakeHealing(int healing, Action onResolve = null) {
        if (_myStats) {
            if (_myStats.IsDead) {
                return;
            }

            _myStats.TakeHealing(healing);
        }

        if (_flashTarget) {
            _flashTarget.StartFlash(true);
        }

        onResolve?.Invoke();
    }
}
