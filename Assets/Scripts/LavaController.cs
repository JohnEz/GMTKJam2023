using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTicker {
    public float timeStarted = 0;
    public float tickCounter = 0;
}

public class LavaController : MonoBehaviour {
    private Dictionary<Damagable, BurnTicker> _targets;

    [SerializeField]
    private float _tickRate = .3f;

    private AudioClip _burnSFX;

    private void Awake() {
        _targets = new Dictionary<Damagable, BurnTicker>();
    }

    private void Update() {
        foreach (Damagable target in _targets.Keys) {
            int timesShouldHaveTicked = (int)(Mathf.Floor((Time.time - _targets[target].timeStarted) / _tickRate));

            if (timesShouldHaveTicked > _targets[target].tickCounter) {
                Tick(target);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
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

        AudioManager.Instance.PlaySound(_burnSFX, transform.position);
    }
}
