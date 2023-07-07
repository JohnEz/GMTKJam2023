using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterController : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform _weaponAnchor;

    private float _targetRange = 3f;
    private float RANGE_TOLERANCE = 0.5f;

    private bool isCircleClockwise = true;

    private CharacterMovement _movement;

    private void Awake() {
        _movement = GetComponent<CharacterMovement>();
    }

    public void Update() {
        MovementLogic();
        AimingLogic();
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

    private void AimingLogic() {
        Vector3 targetDirection = GetTargetPosition() - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        _weaponAnchor.rotation = rotation;
    }

    private Vector3 MoveTowardsTargetDistance() {
        float moveCloser = GetDistanceToTarget() > _targetRange ? 1 : -1;

        Vector3 directionToTarget = GetDirectionToTarget();

        return directionToTarget * moveCloser;
    }

    private Vector3 RunAroundTarget() {
        float circlingAngle = isCircleClockwise ? Mathf.PI / 2 : 3 * Mathf.PI / 2;

        Vector3 directionToTarget = GetDirectionToTarget();

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) + circlingAngle;
        Vector3 targetDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

        return targetDirection;
    }

    private float GetDistanceToTarget() {
        return Vector3.Distance(transform.position, GetTargetPosition());
    }

    private Vector3 GetTargetPosition() {
        return target != null ? target.position : Vector3.zero;
    }

    private Vector3 GetDirectionToTarget() {
        return (GetTargetPosition() - transform.position).normalized;
    }
}
