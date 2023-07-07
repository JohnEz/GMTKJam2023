using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private ControlScheme _controlScheme;

    [SerializeField]
    private PlayerStats _playerStats;

    void Awake() {
        _controlScheme = GetComponent<ControlScheme>();

        if (!_controlScheme) {
            Debug.Log("PlayerMovement: No control scheme found on player!");
        }
    }

    void Update() {
        Vector3 direction = MoveDirection;

        if (direction.magnitude != 0) {
            transform.position += direction * _playerStats.MoveSpeed * Time.deltaTime;
        }
    }

    private Vector3 MoveDirection {
        get {
            Vector3 yDirection = _controlScheme.moveUp
                ? _controlScheme.moveDown
                ? default
                : Vector3.up
                : _controlScheme.moveDown
                ? Vector3.down
                : default;

            Vector3 xDirection = _controlScheme.moveLeft
                ? _controlScheme.moveRight
                ? default
                : Vector3.left
                : _controlScheme.moveRight
                ? Vector3.right
                : default;

            return (xDirection + yDirection).normalized;
        }
    }
}
