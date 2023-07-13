using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerMovementControls : PlayerControls {
    private CharacterMovement _characterMovement;

    public override bool ControlsEnabled => base.ControlsEnabled && _characterMovement;

    protected override void Awake() {
        base.Awake();

        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update() {
        _characterMovement.MoveDirection = MoveDirection;
    }

    private Vector3 MoveDirection {
        get {
            if (!ControlsEnabled) {
                return Vector3.zero;
            }

            bool MoveDown = _controlScheme.MoveDown;
            Vector3 yDirection = _controlScheme.MoveUp
                ? MoveDown
                ? default
                : Vector3.up
                : MoveDown
                ? Vector3.down
                : default;

            bool MoveRight = _controlScheme.MoveRight;
            Vector3 xDirection = _controlScheme.MoveLeft
                ? MoveRight
                ? default
                : Vector3.left
                : MoveRight
                ? Vector3.right
                : default;

            return (xDirection + yDirection).normalized;
        }
    }
}
