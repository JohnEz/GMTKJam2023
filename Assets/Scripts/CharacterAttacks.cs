using System;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour {
    private CharacterStats _myStats;
    private CastController _castController;

    [SerializeField]
    private AudioClip _arrowCastStartSFX;

    public Ability arrowAbility;

    private void Awake() {
        _myStats = GetComponent<CharacterStats>();
        _castController = GetComponent<CastController>();
    }

    public void Attack(Transform target) {
        if (arrowAbility.IsCoolingDown || _castController.IsCasting) {
            return;
        }

        _castController.Cast(arrowAbility, () => {
            try {
                arrowAbility.ClaimCooldown();

                GameObject effectInstance = Instantiate(arrowAbility.Effect, target.position, default);
                Effect effect = effectInstance.GetComponent<Effect>();
                effect.Execute(_myStats);
            } catch (Exception) {
                // Oh well.
            }
        }, () => {
            AudioManager.Instance.PlaySound(_arrowCastStartSFX, transform.position);
        });
    }
}
