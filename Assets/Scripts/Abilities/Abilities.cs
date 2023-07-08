using System;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {
    [SerializeField]
    private List<Ability> _abilities;

    private CastController _castController;

    public void Awake() {
        _castController = GetComponentInParent<CastController>();
    }

    public void TryExecute(int index, Vector3 targetPosition) {
        Ability ability = GetAbility(index);
        if (ability == null) {
            throw new Exception("Abilities: Invalid ability specified to execute!");
        }

        if (_castController.IsCasting) {
            throw new Exception("Abilities: Attempt to execute ability while casting!");
        }

        _castController.Cast(ability, () => {
            try {
                ability.ClaimCooldown();

                GameObject effectInstance = Instantiate(ability.Effect, targetPosition, default);
                Effect effect = effectInstance.GetComponent<Effect>();
                effect.Execute(transform.parent);
            } catch (Exception) {
                // Oh well.
            }
        }, null);
    }

    private Ability GetAbility(int index) {
        if (index < 0 || index >= _abilities.Count) {
            return null;
        }

        return _abilities[index];
    }
}
