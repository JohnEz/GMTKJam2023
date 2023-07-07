using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterMovement : MonoBehaviour {
    public Vector3 MoveDirection { get; set; }

    [SerializeField]
    private Animator _myAnimator;

    [SerializeField]
    private CharacterStats _playerStats;

    public void Awake() {
        _playerStats = GetComponent<CharacterStats>();
    }

    public void Update() {
        if (_myAnimator) {
            _myAnimator.SetFloat("moveVelocity", MoveDirection.magnitude);
        }

        transform.position += MoveDirection * _playerStats.MoveSpeed * Time.deltaTime;
    }
}
