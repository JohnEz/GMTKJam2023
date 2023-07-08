using System;
using UnityEngine;

[Serializable]
public class Ability {
    public float CastTime = 0.4f;

    [SerializeField]
    private int _cooldown = 0;

    [SerializeField]
    private GameObject _effect;

    public GameObject Effect => _effect;

    private float _timeOffCooldown = 0;

    private bool _isCoolingDown => _timeOffCooldown > Time.time;

    public bool IsCoolingDown { get => _isCoolingDown; }

    public void StartCooldown() {
        _timeOffCooldown = Time.time + _cooldown;
    }
}
