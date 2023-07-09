using UnityEngine;

public class CharacterAttacks : MonoBehaviour {
    private CastController _castController;

    [SerializeField]
    private AudioClip _arrowCastStartSFX;

    public Ability arrowAbility;

    [SerializeField]
    private float _cooldownRandom = 0;

    private void Awake() {
        _castController = GetComponent<CastController>();
    }

    public void Attack(Transform target) {
        if (arrowAbility.IsCoolingDown || _castController.IsCasting) {
            return;
        }

        _castController.Cast(arrowAbility, () => {
            float cooldownReduction = Random.Range(1, 1 + _cooldownRandom);

            arrowAbility.StartCooldown(cooldownReduction);

            GameObject effectsInstance = Instantiate(arrowAbility.Effects, target.position, default);
            Effects effects = effectsInstance.GetComponent<Effects>();
            effects.Execute(transform);
        }, () => {
            AudioManager.Instance.PlaySound(_arrowCastStartSFX, transform.position);
        });
    }
}
