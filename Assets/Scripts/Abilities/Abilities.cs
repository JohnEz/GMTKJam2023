using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {

    [SerializeField]
    private List<Ability> _abilities;

    public List<Ability> AbilitiesList { get => _abilities; }

    private CastController _castController;

    public void Awake() {
        _castController = GetComponentInParent<CastController>();
    }

    public void TryExecute(int index, Vector3 targetPosition) {
        Ability ability = GetAbility(index);
        if (ability == null || !ability.Enabled || _castController.IsCasting || ability.IsCoolingDown) {
            return;
        }

        _castController.Cast(ability, () => {
            ability.StartCooldown();

            GameObject effectsInstance = Instantiate(ability.Effects, targetPosition, default);
            Effects effects = effectsInstance.GetComponent<Effects>();
            effects.Execute(transform.parent);
        }, null);

        return;
    }

    public void EnableAbility(int index) {
        if (index <= _abilities.Count) {
            _abilities[index].Enabled = true;
        }
    }

    private Ability GetAbility(int index) {
        if (index < 0 || index >= _abilities.Count) {
            return null;
        }

        return _abilities[index];
    }
}
