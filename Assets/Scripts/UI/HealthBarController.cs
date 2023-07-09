using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
    [SerializeField]
    private List<Color> _healthBarColours;

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
        SetHealthBarColour(0);
    }

    public void OnEnable() {
        if (_myStats != null) {
            _myStats.OnHealthChanged += SetHp;
            _myStats.OnHealthBarDepleted += OnHealthBarDepleted;
        }
    }

    public void OnDisable() {
        if (_myStats != null) {
            _myStats.OnHealthChanged -= SetHp;
            _myStats.OnHealthBarDepleted -= OnHealthBarDepleted;
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

    public void OnHealthBarDepleted(int index) {
        SetHealthBarColour(index + 1);
    }

    private void SetHealthBarColour(int colourIndex) {
        healthBar.color = _healthBarColours[Mathf.Min(colourIndex, _healthBarColours.Count - 1)];
    }
}
