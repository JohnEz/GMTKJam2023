using System;
using UnityEngine;

[Serializable]
public class Ability {
    public float CastTime = 0.4f;

    [SerializeField]
    private float _cooldown = 0;

    [SerializeField]
    private GameObject _effects;

    public GameObject Effects => _effects;

    private float _timeOffCooldown = 0;

    private bool _isCoolingDown => _timeOffCooldown > Time.time;

    public bool IsCoolingDown { get => _isCoolingDown; }

    public void StartCooldown(float cooldownReduction = 1) {
        _timeOffCooldown = Time.time + (_cooldown * cooldownReduction);
    }
}
