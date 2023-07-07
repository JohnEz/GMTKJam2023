using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector3 Direction { get; set; }

    [SerializeField]
    private float MOVE_SPEED = 20f;

    public void Setup(Vector3 direction) {
        Direction = direction;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }

    // Update is called once per frame
    private void Update() {
        transform.position += Direction * MOVE_SPEED * Time.deltaTime;
    }
}
