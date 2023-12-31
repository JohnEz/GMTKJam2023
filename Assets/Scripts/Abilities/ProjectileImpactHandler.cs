using System.Collections;
using UnityEngine;

public class ProjectileImpactHandler : MonoBehaviour {
    private static readonly float s_collisionDuration = 0.5f;

    private Collider2D _collider;

    private Projectile _projectile;

    [SerializeField]
    private AudioClip _impactSFX;

    [SerializeField]
    private bool _shiftPitch = false;

    public void Awake() {
        _collider = GetComponent<Collider2D>();
        _projectile = GetComponentInParent<Projectile>();
    }

    public void OnImpact() {
        _collider.enabled = true;

        _projectile.OnImpact();

        AudioManager.Instance.PlaySound(_impactSFX, transform.position, _shiftPitch);

        StartCoroutine(DisableCollision());
    }

    private IEnumerator DisableCollision() {
        yield return new WaitForSeconds(s_collisionDuration);
        _collider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        // Yikes
        _projectile.OnTriggerEnter2D(collision);
    }
}
