using System.Collections;
using UnityEngine;

public class ArrowEffect : Effect {

    [SerializeField]
    private GameObject _arrowPrefab;

    [SerializeField]
    private AudioClip _attackSound;

    public override void Execute(CharacterStats originatorStats) {
        base.Execute(originatorStats);

        LaunchProjectile();
    }

    private void LaunchProjectile() {
        GameObject arrow = Instantiate(_arrowPrefab);
        arrow.transform.position = _originatorStats.transform.position;

        Projectile projectile = arrow.GetComponent<Projectile>();
        Vector3 direction = transform.position - arrow.transform.position;

        projectile.Setup(direction, _originatorStats);
    }
}
