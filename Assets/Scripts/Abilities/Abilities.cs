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
            return;
        }

        if (_castController.IsCasting || ability.IsCoolingDown) {
            return;
        }

        _castController.Cast(ability, () => {
            ability.StartCooldown();

            GameObject effectInstance = Instantiate(ability.Effect, targetPosition, default);
            Effect effect = effectInstance.GetComponent<Effect>();
            effect.Execute(transform.parent);
        }, null);

        return;
    }

    private Ability GetAbility(int index) {
        if (index < 0 || index >= _abilities.Count) {
            return null;
        }

        return _abilities[index];
    }
}
