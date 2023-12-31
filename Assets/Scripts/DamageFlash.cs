using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageFlash : MonoBehaviour {

    [SerializeField]
    private Color _flashColor = Color.white;

    [SerializeField]
    private Color _flashHealColor = Color.green;

    [SerializeField]
    private float _flashTime = 0.15f;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;

    [SerializeField]
    private AnimationCurve _flashCurve;

    private void Awake() {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        Init();
    }

    private void Init() {
        _materials = new Material[_spriteRenderers.Length];

        for (int i = 0; i < _spriteRenderers.Length; i++) {
            _materials[i] = _spriteRenderers[i].material;
        }
    }

    public void StartFlash(bool isHeal = false) {
        SetFlashColor(isHeal ? _flashHealColor : _flashColor);

        DOVirtual.Float(0, 1, _flashTime, value => SetFlashAmount(value)).SetEase(_flashCurve);
    }

    private void SetFlashColor(Color color) {
        foreach (Material mat in _materials) {
            mat.SetColor("_FlashColor", color);
        }
    }

    private void SetFlashAmount(float amount) {
        foreach (Material mat in _materials) {
            mat.SetFloat("_FlashAmount", amount);
        }
    }
}
