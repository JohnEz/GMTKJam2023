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
        if (ability == null) {
            return;
        }

        if (_castController.IsCasting || ability.IsCoolingDown) {
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

    private Ability GetAbility(int index) {
        if (index < 0 || index >= _abilities.Count) {
            return null;
        }

        return _abilities[index];
    }
}
