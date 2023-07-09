using System;
using UnityEngine;

[Serializable]
public class Ability {
    public float CastTime = 0.4f;

    [SerializeField]
    private float _cooldown = 0;

    public float Cooldown { get => _cooldown; }

    [SerializeField]
    private GameObject _effects;

    public GameObject Effects => _effects;

    public float TimeOffCooldown { get; set; }

    private bool _isCoolingDown => TimeOffCooldown > Time.time;

    public bool IsCoolingDown { get => _isCoolingDown; }

    public void StartCooldown(float cooldownReduction = 1) {
        TimeOffCooldown = Time.time + (_cooldown * cooldownReduction);
    }
}
