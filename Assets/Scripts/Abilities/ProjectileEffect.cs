using UnityEngine;

public class ProjectileEffect : Effect {
    [SerializeField]
    protected GameObject _projectilePrefab;

    protected override void RunEffect(Transform origin) {
        GameObject projectileInstance = Instantiate(_projectilePrefab);
        projectileInstance.transform.position = origin.position;

        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        projectile.Launch(origin, transform.position);
    }
}
