using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour {
    private List<Damagable> _targets;

    [SerializeField]
    private float _tickRate = .3f;

    [SerializeField]
    private AudioClip _burnSFX;

    private void Awake() {
        _targets = new();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
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

        hitDamagable.TakeDamage(5, () => {
            AudioManager.Instance.PlaySound(_burnSFX, transform.position);
            StartCoroutine(QueueNextTick(hitDamagable));
        });
    }

    private IEnumerator QueueNextTick(Damagable hitDamagable) {
        yield return new WaitForSeconds(_tickRate);
        Tick(hitDamagable);
    }
}
