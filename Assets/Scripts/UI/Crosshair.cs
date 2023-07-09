using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Crosshair : MonoBehaviour {

    [SerializeField]
    private Image leftClickBar;

    [SerializeField]
    private Image rightClickBar;

    [SerializeField]
    private Image spacebarBar;

    [SerializeField]
    private Abilities abilities;

    [SerializeField]
    private AnimationCurve pulseCurve;

    private void Awake() {
        leftClickBar.fillAmount = 0;
        rightClickBar.fillAmount = 0;
        spacebarBar.fillAmount = 0;

        Cursor.visible = false;
    }

    public void Update() {
        FollowMouse();

        if (abilities.AbilitiesList[0] != null) {
            leftClickBar.fillAmount = CalculateBarFill(abilities.AbilitiesList[0]);
        }

        if (abilities.AbilitiesList[1] != null) {
            rightClickBar.fillAmount = CalculateBarFill(abilities.AbilitiesList[1]);
        }

        if (abilities.AbilitiesList[2] != null) {
            spacebarBar.fillAmount = CalculateBarFill(abilities.AbilitiesList[2]);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Pulse();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            Pulse();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Pulse();
        }
    }

    private void Pulse() {
        Sequence sequence = DOTween.Sequence();

        sequence.Join(transform.DOScale(new Vector3(80f, 80f, 80f), .15f));
        sequence.Join(transform.DOScale(new Vector3(24f, 24f, 24f), .15f));
    }

    private void FollowMouse() {
        transform.position = Input.mousePosition;
    }

    private float CalculateBarFill(Ability abil) {
        if (!abil.IsCoolingDown) {
            return 1f;
        }

        float remainingCooldown = abil.TimeOffCooldown - Time.time;

        return 1 - (remainingCooldown / abil.Cooldown);
    }
}
