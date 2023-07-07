using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour {

    [SerializeField]
    private GameObject arrowPrefab;

    public void Attack(Transform target) {
        FireArrow(target.position - transform.position);
    }

    private void FireArrow(Vector3 direction) {
        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = transform.position;
        Projectile projectile = arrow.GetComponent<Projectile>();

        projectile.Setup(direction);
    }
}
