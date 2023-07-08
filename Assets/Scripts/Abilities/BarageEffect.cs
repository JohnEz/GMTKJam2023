using System.Collections;
using UnityEngine;

public class BarageEffect : Effect {
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private AudioClip _attackSound;

    [SerializeField]
    private int _numProjectiles;

    [SerializeField]
    private float _launchPeriodSeconds;

    public override void Execute(CharacterStats originatorStats) {
        base.Execute(originatorStats);

        StartCoroutine(LaunchProjectiles());
    }

    private IEnumerator LaunchProjectiles() {
        for (int i = 0; i < _numProjectiles; i++) {
            LaunchProjectile();
            yield return new WaitForSeconds(_launchPeriodSeconds);
        }
    }

    private void LaunchProjectile() {
        GameObject arrow = Instantiate(_projectilePrefab);
        arrow.transform.position = _originatorStats.transform.position;

        Vector3 direction = transform.position - arrow.transform.position;
        Projectile projectile = arrow.GetComponent<Projectile>();

        projectile.Setup(direction, _originatorStats);

        AudioManager.Instance.PlaySound(_attackSound, transform.position);
    }
}
