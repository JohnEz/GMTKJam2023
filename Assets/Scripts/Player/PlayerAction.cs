using UnityEngine;

public class PlayerAction : MonoBehaviour {
    private PlayerControls _playerControls;

    public void Awake() {
        _playerControls = GetComponent<PlayerControls>();
    }

    void Update() {
        if (_playerControls.Ability1) {
            Debug.Log("ABILITY ONE AT ");
            Debug.Log(_playerControls.TargetPosition);
        } else if (_playerControls.Ability2) {
            Debug.Log("ABILITY TWO AT ");
            Debug.Log(_playerControls.TargetPosition);
        }
    }
}
