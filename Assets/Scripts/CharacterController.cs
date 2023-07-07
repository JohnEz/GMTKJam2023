using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterController : MonoBehaviour {
    private Transform target;

    private float _targetRange = 2f;
    private float RANGE_TOLERANCE = 0.5f;

    private float rotationDirection = 1;

    private CharacterMovement _movement;

    private void Awake() {
        _movement = GetComponent<CharacterMovement>();
    }

    public void Update() {
        MovementLogic();
    }

    private void MovementLogic() {
        Vector3 moveDirection = Vector3.zero;

        if (Mathf.Abs(GetDistanceToTarget() - _targetRange) > RANGE_TOLERANCE) {
            moveDirection = MoveTowardsTargetDistance();
        } else {
            moveDirection = RunAroundTarget();
        }

        _movement.MoveDirection = moveDirection;
    }

    private Vector3 MoveTowardsTargetDistance() {
        float moveCloser = GetDistanceToTarget() > _targetRange ? 1 : -1;

        Vector3 directionToTarget = (GetTargetPosition() - transform.position).normalized;

        return directionToTarget;
    }

    private Vector3 RunAroundTarget() {
        return Vector3.zero;
    }

    private float GetDistanceToTarget() {
        return Vector3.Distance(transform.position, GetTargetPosition());
    }

    private Vector3 GetTargetPosition() {
        return target != null ? target.position : Vector3.zero;
    }
}
