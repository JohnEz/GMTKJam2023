using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour {

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Image damageBar;

    [SerializeField]
    private TMP_Text nameText;

    private const float DAMAGE_BAR_SHRINK_TIMER_MAX = 0.3f;
    private const float BAR_SHRINK_SPEED = 5f;

    private float damageBarShrinkTimer;
    private float targetHealthPercent = 1;

    [SerializeField]
    private CharacterStats _myStats;

    private void Awake() {
        SetHp();

        if (nameText) {
            nameText.text = _myStats.Name;
        }
    }

    public void OnEnable() {
        if (_myStats != null) {
            _myStats.OnHealthChanged += SetHp;
        }
    }

    public void OnDisable() {
        if (_myStats != null) {
            _myStats.OnHealthChanged -= SetHp;
        }
    }

    // Update is called once per frame
    private void Update() {
        if (damageBarShrinkTimer < 0) {
            UpdateBarToValue(damageBar, healthBar.fillAmount);
        } else {
            damageBarShrinkTimer -= Time.deltaTime;
        }
    }

    public static void UpdateBarToValue(Image bar, float targetAmount) {
        if (targetAmount == bar.fillAmount) {
            return;
        }
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, targetAmount, BAR_SHRINK_SPEED * Time.deltaTime);

        float distance = Mathf.Abs(targetAmount - bar.fillAmount);
        if (distance < 0.005f) {
            bar.fillAmount = targetAmount;
        }
    }

    public void SetHp() {
        float health = _myStats.CurrentHealth;
        float maxHealth = _myStats.MaxHealth;

        targetHealthPercent = health / maxHealth;

        healthBar.fillAmount = targetHealthPercent;
        damageBar.fillAmount = Mathf.Max(damageBar.fillAmount, targetHealthPercent);

        damageBarShrinkTimer = DAMAGE_BAR_SHRINK_TIMER_MAX;
    }
}
