using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {
    private List<Damagable> _targets;

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
        _targets = new();
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
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (!_isActive) {
            return;
        }

        Damagable hitDamagable = collision.gameObject.GetComponent<Damagable>();

        if (!hitDamagable || _targets.Contains(hitDamagable)) {
            return;
        }

        _targets.Add(hitDamagable);

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
        if (!_targets.Contains(hitDamagable)) {
            return;
        }

        hitDamagable.TakeDamage(3, () => {
            StartCoroutine(QueueNextTick(hitDamagable));
        });
    }

    private IEnumerator QueueNextTick(Damagable hitDamagable) {
        yield return new WaitForSeconds(_tickRate);
        Tick(hitDamagable);
    }
}
