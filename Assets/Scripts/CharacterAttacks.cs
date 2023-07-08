using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour {

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private float _cooldownDuration = 2f;

    private float _timeOffCooldown = 0;

    public void Attack(Transform target) {
        if (IsAttackOnCooldown()) {
            return;
        }

        _timeOffCooldown = Time.time + _cooldownDuration;
        FireArrow(target.position - transform.position);
    }

    private bool IsAttackOnCooldown() {
        return _timeOffCooldown > Time.time;
    }

    private void FireArrow(Vector3 direction) {
        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = transform.position;
        Projectile projectile = arrow.GetComponent<Projectile>();

        projectile.Setup(direction);
    }
}
