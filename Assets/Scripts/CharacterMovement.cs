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

    public void Update() {
        if (_myAnimator) {
            _myAnimator.SetFloat("moveVelocity", MoveDirection.magnitude);
        }

        Vector3 newPosition = transform.position + MoveDirection * _playerStats.MoveSpeed * Time.deltaTime;
        _body.MovePosition(newPosition);
    }
}
