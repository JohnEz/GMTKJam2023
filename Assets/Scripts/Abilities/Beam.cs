using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {
    private Dictionary<Damagable, BurnTicker> _targets;

    private Transform _target;

    private float _targetAngle;

    [SerializeField]
    private float _duration = 1.5f;

    private float _fadeOutTimer = .5f;

    private bool _isActive = false;

    private float _tickRate = .1f;

    [SerializeField]
    private Animator _animator;

    private void Awake() {
        _targets = new Dictionary<Damagable, BurnTicker>();
    }

    public void Setup(Transform target) {
        _target = target;

        Vector3 targetDirection = target.position - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;

        _isActive = true;

        CameraManager.Instance.ShakeCamera(5, _duration + (_fadeOutTimer / 2));

        Invoke("FadeOutBeam", _duration);
    }

    private void FaceMouse() {
    }

    private void FadeOutBeam() {
        _isActive = false;

        _animator?.SetTrigger("fadeOut");

        Destroy(gameObject, _fadeOutTimer);
    }

    private void Update() {
        if (!_isActive) {
            return;
        }

        FaceMouse();

        foreach (Damagable target in _targets.Keys) {
            int timesShouldHaveTicked = (int)(Mathf.Floor((Time.time - _targets[target].timeStarted) / _tickRate));

            if (timesShouldHaveTicked > _targets[target].tickCounter) {
                Tick(target);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (!_isActive) {
            return;
        }

        Damagable hitDamagable = collision.gameObject.GetComponent<Damagable>();

        if (!hitDamagable) {
            return;
        }

        if (_targets.ContainsKey(hitDamagable)) {
            return;
        }

        BurnTicker newTicker = new BurnTicker();
        newTicker.timeStarted = Time.time;
        _targets.Add(hitDamagable, newTicker);

        Tick(hitDamagable);
    }

    public void OnTriggerExit2D(Collider2D other) {
        Damagable hitDamagable = other.gameObject.GetComponent<Damagable>();

        if (!hitDamagable) {
            return;
        }

        _targets.Remove(hitDamagable);
    }

    private void Tick(Damagable hitDamagable) {
        if (!_targets.ContainsKey(hitDamagable)) {
            return;
        }

        CharacterStats hitStats = hitDamagable.GetComponent<CharacterStats>();

        if (hitStats && hitStats.IsDead) {
            return;
        }

        BurnTicker ticker = _targets[hitDamagable];

        ticker.tickCounter += 1;

        hitDamagable.TakeDamage(5);
    }
}
