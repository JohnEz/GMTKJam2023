using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterMovement : MonoBehaviour {
    public Vector3 MoveDirection { get; set; }
    private Rigidbody2D _body;

    [SerializeField]
    private Animator _myAnimator;

    [SerializeField]
    private CharacterStats _playerStats;

    public void Awake() {
        _playerStats = GetComponent<CharacterStats>();
        _body = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        if (_myAnimator) {
            _myAnimator.SetFloat("moveVelocity", MoveDirection.magnitude);
        }

        Vector2 moveDirection = MoveDirection;

        Vector3 newPosition = _body.position + (moveDirection.normalized * _playerStats.MoveSpeed) * Time.fixedDeltaTime;

        _body.MovePosition(newPosition);
    }
}
