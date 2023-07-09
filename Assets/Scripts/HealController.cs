using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour {
    private Dictionary<Damagable, Ticker> _targets;

    [SerializeField]
    private float _tickRate = .2f;

    private AudioClip _burnSFX;

    private void Awake() {
        _targets = new Dictionary<Damagable, Ticker>();
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

        Ticker newTicker = new Ticker();
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

        Ticker ticker = _targets[hitDamagable];

        ticker.tickCounter += 1;

        hitDamagable.TakeHealing(1);

        AudioManager.Instance.PlaySound(_burnSFX, transform.position);
    }
}