using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerMovement : MonoBehaviour {
    private ControlScheme _controlScheme;

    private CharacterMovement _characterMovement;

    void Awake() {
        _controlScheme = GetComponent<ControlScheme>();
        _characterMovement = GetComponent<CharacterMovement>();

        if (!_controlScheme) {
            Debug.Log("PlayerMovement: No control scheme found on player!");
        }
    }

    void Update() {
        _characterMovement.MoveDirection = MoveDirection;
    }

    private Vector3 MoveDirection {
        get {
            Vector3 yDirection = _controlScheme.MoveUp
                ? _controlScheme.MoveDown
                ? default
                : Vector3.up
                : _controlScheme.MoveDown
                ? Vector3.down
                : default;

            Vector3 xDirection = _controlScheme.MoveLeft
                ? _controlScheme.MoveRight
                ? default
                : Vector3.left
                : _controlScheme.MoveRight
                ? Vector3.right
                : default;

            return (xDirection + yDirection).normalized;
        }
    }
}
