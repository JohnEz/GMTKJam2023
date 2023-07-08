using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector3 Direction { get; set; }

    [SerializeField]
    private float MOVE_SPEED = 20f;

    private CharacterStats _caster;

    [SerializeField]
    private AudioClip _onSpawnSFX;

    [SerializeField]
    private AudioClip _onloopSFX;

    [SerializeField]
    private AudioClip _onHitSFX;

    public void Setup(Vector3 direction, CharacterStats caster) {
        _caster = caster;

        Direction = direction.normalized;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;

        AudioManager.Instance.PlaySound(_onSpawnSFX, transform.position);

        AudioManager.Instance.PlaySound(_onloopSFX, transform);
    }

    // Update is called once per frame
    private void Update() {
        transform.position += Direction * MOVE_SPEED * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Damagable hitDamagable = collision.gameObject.GetComponent<Damagable>();

        if (!hitDamagable) {
            return;
        }

        CharacterStats hitCharacter = hitDamagable.GetComponent<CharacterStats>();

        if (hitCharacter && hitCharacter != _caster) {
            hitDamagable.TakeDamage(10);

            AudioManager.Instance.PlaySound(_onHitSFX, transform.position);

            Destroy(gameObject);
        }
    }
}
