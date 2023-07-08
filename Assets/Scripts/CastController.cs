using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

internal struct CastingAbility {
    public Ability ability;
    public Action onCastStart;
    public Action onCastComplete;
}

public class CastController : MonoBehaviour {

    [SerializeField]
    private CharacterStats _myStats;

    public bool IsCasting = false;

    private Ability _castingAbility;
    private bool _castRequest;
    private bool _castSuccess;

    private float _castTime = 0;

    public UnityEvent<Ability> OnCastStart = new UnityEvent<Ability>();

    public UnityEvent OnCastFail = new UnityEvent();

    public UnityEvent OnCastSuccess = new UnityEvent();

    private CastingAbility _abilityToCast;

    public void Awake() {
        _myStats.OnDeath += HandleDeath;
    }

    public void HandleDeath() {
        if (_castRequest) {
            CastFail();
        }
    }

    public bool Cast(Ability ability, Action onCastComplete, Action onCastStart = null) {
        if (IsCasting || _myStats.IsDead) {
            return false;
        }

        _abilityToCast = new CastingAbility();

        _abilityToCast.ability = ability;
        _abilityToCast.onCastStart = onCastStart;
        _abilityToCast.onCastComplete = onCastComplete;

        _castTime = ability.CastTime;
        _castingAbility = ability;
        StartCoroutine(Casting());
        OnCastStart.Invoke(ability);

        return true;
    }

    private IEnumerator Casting() {
        IsCasting = true;

        RequestCast();

        if (_abilityToCast.onCastStart != null) {
            _abilityToCast.onCastStart();
        }

        yield return new WaitUntil(() => _castRequest == false);

        if (_castSuccess) {
            _abilityToCast.onCastComplete();
        } else {
            // do something if it failed, show interrupted icon or something
        }

        IsCasting = false;
    }

    private void RequestCast() {
        _castRequest = true;
        _castSuccess = false;
        Invoke("CastSuccess", _castTime);
    }

    private void CastSuccess() {
        _castRequest = false;
        _castSuccess = true;
        OnCastSuccess?.Invoke();
    }

    public void CastFail() {
        _castRequest = false;
        _castSuccess = false;
        CancelInvoke("CastSuccess");
        OnCastFail?.Invoke();
    }
}
