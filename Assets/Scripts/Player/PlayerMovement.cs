using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerMovement : MonoBehaviour {
    private PlayerControls _playerControls;

    private CharacterMovement _characterMovement;

    void Awake() {
        _playerControls = GetComponent<PlayerControls>();
        _characterMovement = GetComponent<CharacterMovement>();

        if (!_playerControls) {
            Debug.Log("PlayerMovement: No control scheme found on player!");
        }
    }

    void Update() {
        _characterMovement.MoveDirection = MoveDirection;
    }

    private Vector3 MoveDirection {
        get {
            Vector3 yDirection = _playerControls.MoveUp
                ? _playerControls.MoveDown
                ? default
                : Vector3.up
                : _playerControls.MoveDown
                ? Vector3.down
                : default;

            Vector3 xDirection = _playerControls.MoveLeft
                ? _playerControls.MoveRight
                ? default
                : Vector3.left
                : _playerControls.MoveRight
                ? Vector3.right
                : default;

            return (xDirection + yDirection).normalized;
        }
    }
}
