using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterController : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform _weaponAnchor;

    private float MIN_RANGE = 3f;
    private float MAX_RANGE = 10f;

    private float _targetRange = 9f;
    private float RANGE_TOLERANCE = 0.5f;

    private bool isCircleClockwise = true;

    private CharacterMovement _movement;
    private CharacterAttacks _attacks;
    private CharacterStats _myStats;

    [SerializeField]
    private Animator _animator;

    private float RANDOM_ACTION_DELAY = 5f;
    private float RANDOM_ACTION_VARIANCE = .33f;

    private float randomActionTimer = 0f;
    private float currentrandomActionDelay = 5f;

    private void Awake() {
        _movement = GetComponent<CharacterMovement>();
        _attacks = GetComponent<CharacterAttacks>();
        _myStats = GetComponent<CharacterStats>();

        _myStats.OnDeath += HandleDeath;

        ChangeRange();
    }

    public void Update() {
        if (_myStats.IsDead || !GameManager.Instance.isGameActive()) {
            GetComponent<CircleCollider2D>().enabled = false;
            return;
        }

        GetComponent<CircleCollider2D>().enabled = true;

        CalculateRandomAction();
        MovementLogic();
        AimingLogic();

        // temp
        _attacks.Attack(target);
    }

    private void CalculateRandomAction() {
        randomActionTimer += Time.deltaTime;

        if (randomActionTimer >= currentrandomActionDelay) {
            randomActionTimer = 0f;
            currentrandomActionDelay = Random.Range(RANDOM_ACTION_DELAY * (1 - RANDOM_ACTION_VARIANCE), RANDOM_ACTION_DELAY * (1 + RANDOM_ACTION_VARIANCE));

            // choose a random action
            float result = Random.value;

            if (result >= 0.5) {
                FlipRotationDirection();
            } else {
                ChangeRange();
            }
        }
    }

    private void FlipRotationDirection() {
        isCircleClockwise = !isCircleClockwise;
    }

    private void ChangeRange() {
        _targetRange = Random.Range(MIN_RANGE, MAX_RANGE);
    }

    private void HandleDeath() {
        _movement.MoveDirection = Vector3.zero;
        _animator.SetTrigger("onDeath");
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
