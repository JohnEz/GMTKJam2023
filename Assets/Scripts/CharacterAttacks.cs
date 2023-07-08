using UnityEngine;

public class CharacterAttacks : MonoBehaviour {
    private CharacterStats _myStats;
    private CastController _castController;

    [SerializeField]
    private AudioClip arrowCastSFX;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private AudioClip _attackSound;

    [SerializeField]
    private float _cooldownDuration = 2f;

    private float _timeOffCooldown = 0;

    public Ability arrowAbility;

    private void Awake() {
        _myStats = GetComponent<CharacterStats>();
        _castController = GetComponent<CastController>();
    }

    public void Attack(Transform target) {
        if (IsAttackOnCooldown() || _castController.IsCasting) {
            return;
        }

        _castController.Cast(arrowAbility, () => {
            _timeOffCooldown = Time.time + _cooldownDuration;
            FireArrow(target.position - transform.position);
        }, () => {
            AudioManager.Instance.PlaySound(arrowCastSFX, transform);
        });
    }

    private bool IsAttackOnCooldown() {
        return _timeOffCooldown > Time.time;
    }

    private void FireArrow(Vector3 direction) {
        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = transform.position;
        Projectile projectile = arrow.GetComponent<Projectile>();

        projectile.Setup(direction, _myStats);

        AudioManager.Instance.PlaySound(_attackSound, transform.position);
    }
}
