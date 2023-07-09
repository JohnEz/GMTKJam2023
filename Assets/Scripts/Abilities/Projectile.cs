using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector3 Direction { get; set; }

    [SerializeField]
    private float MOVE_SPEED = 20f;

    private Transform _origin;

    [SerializeField]
    private AudioClip _onSpawnSFX;

    [SerializeField]
    private AudioClip _onloopSFX;

    [SerializeField]
    private AudioClip _onHitSFX;

    [SerializeField]
    private bool _destroyOnImpact = true;

    [SerializeField]
    private GameObject _missEffects;

    [SerializeField]
    private GameObject _impactEffects;

    [SerializeField]
    private float _maxDistance = -1f;

    private float _totalDistance = 0f;

    [SerializeField]
    private int _damage = 10;

    public void Launch(Transform origin, Vector3 target) {
        _origin = origin;

        Direction = (target - origin.position).normalized;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;

        AudioManager.Instance.PlaySound(_onSpawnSFX, transform.position);
        AudioManager.Instance.PlaySound(_onloopSFX, transform);
    }

    // Update is called once per frame
    private void Update() {
        float distance = MOVE_SPEED * Time.deltaTime;
        if (_maxDistance >= 0) {
            distance = Mathf.Clamp(distance, distance, _maxDistance - _totalDistance);
        }

        _totalDistance += distance;

        transform.position += Direction * distance;

        if (_maxDistance >= 0 && _totalDistance >= _maxDistance) {
            OnMiss();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Damagable hitDamagable = collision.gameObject.GetComponent<Damagable>();

        if (!hitDamagable) {
            return;
        }

        Damagable originDamagable = _origin.GetComponent<Damagable>();

        if (hitDamagable != originDamagable) {
            hitDamagable.TakeDamage(_damage);

            AudioManager.Instance.PlaySound(_onHitSFX, transform.position);

            if (_destroyOnImpact) {
                Destroy(gameObject);
            }
        }
    }

    public void OnImpact() {
        if (_impactEffects) {
            GameObject effectsInstance = Instantiate(_impactEffects, transform.position, default);
            Effects effects = effectsInstance.GetComponent<Effects>();
            effects.Execute(transform);
        }
    }

    private void OnMiss() {
        if (_missEffects) {
            GameObject effectsInstance = Instantiate(_missEffects, transform.position, default);
            Effects effects = effectsInstance.GetComponent<Effects>();
            effects.Execute(transform);
        }

        if (_destroyOnImpact) {
            Destroy(gameObject);
        }
    }
}
