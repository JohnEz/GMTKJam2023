using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour {
    private List<Damagable> _targets;

    [SerializeField]
    private float _tickRate = .2f;

    private AudioClip _burnSFX;

    private void Awake() {
        _targets = new();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Damagable hitDamagable = collision.gameObject.GetComponent<Damagable>();

        if (!hitDamagable || _targets.Contains(hitDamagable)) {
            return;
        }

        int myfaction = GetComponentInParent<CharacterStats>().faction;
        int otherFaction = collision.gameObject.GetComponent<CharacterStats>().faction;

        if (myfaction != otherFaction) {
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

        hitDamagable.TakeHealing(1, () => {
            AudioManager.Instance.PlaySound(_burnSFX, transform.position);
            StartCoroutine(QueueNextTick(hitDamagable));
        });

    }

    private IEnumerator QueueNextTick(Damagable hitDamagable) {
        yield return new WaitForSeconds(_tickRate);
        Tick(hitDamagable);
    }
}
