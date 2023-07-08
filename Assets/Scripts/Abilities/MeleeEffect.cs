using UnityEngine;

public class MeleeEffect : ProjectileEffect {
    [SerializeField]
    private Vector3 _originOffset = Vector3.zero;

    protected override void RunEffect(Transform origin) {
        GameObject projectileInstance = Instantiate(_projectilePrefab, origin);
        projectileInstance.transform.localPosition = _originOffset;

        Projectile projectile = projectileInstance.GetComponentInChildren<Projectile>();
        projectile.Launch(origin, transform.position);
    }
}
