using System;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour {
    private CastController _castController;

    [SerializeField]
    private AudioClip _arrowCastStartSFX;

    public Ability arrowAbility;

    private void Awake() {
        _castController = GetComponent<CastController>();
    }

    public void Attack(Transform target) {
        if (arrowAbility.IsCoolingDown || _castController.IsCasting) {
            return;
        }

        _castController.Cast(arrowAbility, () => {
            arrowAbility.StartCooldown();

            GameObject effectInstance = Instantiate(arrowAbility.Effect, target.position, default);
            Effect effect = effectInstance.GetComponent<Effect>();
            effect.Execute(transform);
        }, () => {
            AudioManager.Instance.PlaySound(_arrowCastStartSFX, transform.position);
        });
    }
}
