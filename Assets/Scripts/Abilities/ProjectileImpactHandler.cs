using System.Collections;
using UnityEngine;

public class ProjectileImpactHandler : MonoBehaviour {
    private static readonly float s_collisionDuration = 0.5f;

    private Collider2D _collider;

    private Projectile _projectile;

    public void Awake() {
        _collider = GetComponent<Collider2D>();
        _projectile = GetComponentInParent<Projectile>();
    }

    public void OnImpact() {
        _collider.enabled = true;

        CameraManager.Instance.ShakeCamera(2.5f, .2f);

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
