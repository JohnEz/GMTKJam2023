using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    [SerializeField]
    private Image leftClickBar;

    [SerializeField]
    private Image rightClickBar;

    [SerializeField]
    private Image spacebarBar;

    [SerializeField]
    private Abilities abilities;

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
