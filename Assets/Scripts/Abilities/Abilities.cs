using System;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {
    [SerializeField]
    private List<Ability> _abilities;

    private CharacterStats _characterStats;

    public void Awake() {
        _characterStats = GetComponentInParent<CharacterStats>();
    }

    public void TryExecute(int index, Vector3 targetPosition) {
        Ability ability = GetAbility(index);
        if (ability == null) {
            throw new Exception("Abilities: Invalid ability specified to execute!");
        }

        try {
            ability.ClaimCooldown();

            GameObject effectInstance = Instantiate(ability.Effect, targetPosition, default);
            Effect effect = effectInstance.GetComponent<Effect>();
            effect.Execute(_characterStats);
        } catch (Exception) {
            // Oh well.
        }
    }

    private Ability GetAbility(int index) {
        if (index < 0 || index >= _abilities.Count) {
            return null;
        }

        return _abilities[index];
    }
}
