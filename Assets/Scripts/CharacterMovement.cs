using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    public Vector3 MoveDirection { get; set; }

    [SerializeField]
    private Animator _myAnimator;

    [SerializeField]
    private float MOVE_SPEED = 4f;

    public void Update() {
        if (_myAnimator) {
            _myAnimator.SetFloat("moveVelocity", MoveDirection.magnitude);
        }

        transform.position += MoveDirection * MOVE_SPEED * Time.deltaTime;
    }
}
