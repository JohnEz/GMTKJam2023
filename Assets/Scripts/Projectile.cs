using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector3 Direction { get; set; }

    [SerializeField]
    private float MOVE_SPEED = 20f;

    private CharacterStats _caster;

    public void Setup(Vector3 direction, CharacterStats caster) {
        _caster = caster;

        Direction = direction;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }

    // Update is called once per frame
    private void Update() {
        transform.position += Direction * MOVE_SPEED * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        CharacterStats hitCharacter = collision.gameObject.GetComponent<CharacterStats>();

        if (hitCharacter && hitCharacter != _caster) {
            hitCharacter.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
