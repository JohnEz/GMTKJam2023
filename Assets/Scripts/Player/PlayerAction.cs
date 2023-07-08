using UnityEngine;

public class PlayerAction : MonoBehaviour {
    private PlayerControls _playerControls;

    private Abilities _abilities;

    public void Awake() {
        _playerControls = GetComponent<PlayerControls>();
        _abilities = GetComponentInChildren<Abilities>();
    }

    void Update() {
        int? abilityIndex = _playerControls.Ability1
            ? 0
            : _playerControls.Ability2
            ? 1
            : null;

        if (abilityIndex != null) {
            _abilities.TryExecute(abilityIndex.Value, _playerControls.TargetPosition);
        }
    }
}
