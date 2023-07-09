using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    private CharacterStats _myStats;

    [SerializeField]
    private DamageFlash _flashTarget;

    private void Awake() {
        _myStats = GetComponent<CharacterStats>();
    }

    public void TakeDamage(int damage) {
        if (_myStats) {
            _myStats.TakeDamage(damage);
        }

        if (_flashTarget) {
            _flashTarget.StartFlash();
        }
    }

    public void TakeHealing(int healing) {
        if (_myStats) {
            _myStats.TakeHealing(healing);
        }

        if (_flashTarget) {
            _flashTarget.StartFlash(true);
        }
    }
}
