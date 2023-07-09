using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ability {
    [SerializeField]
    private bool _enabled = true;

    public bool Enabled {
        get => _enabled;
        set {
            _enabled = value;
            _indicators.ForEach((indicator) => indicator.SetActive(_enabled));
        }
    }

    [SerializeField]
    private List<GameObject> _indicators;

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
