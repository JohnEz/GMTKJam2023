using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChecker : MonoBehaviour {
    private Transform _target;

    [SerializeField]
    private GameObject forwardChecker;

    [SerializeField]
    private GameObject backwardChecker;

    [SerializeField]
    private GameObject leftChecker;

    [SerializeField]
    private GameObject rightChecker;

    private bool _isLeftLava = false;
    public bool IsLeftLava { get => _isLeftLava; }

    private bool _isRightLava = false;
    public bool IsRightLava { get => _isRightLava; }

    private bool _isForwardLava = false;
    public bool IsForwardLava { get => _isForwardLava; }

    private bool _isBackwardLava = false;
    public bool IsBackwardLava { get => _isBackwardLava; }

    public void Setup(Transform target) {
        _target = target;
    }

    private void Update() {
        RotateToTarget();
    }

    private void FixedUpdate() {
        UpdateCheckers();
    }

    private void RotateToTarget() {
        if (!_target) {
            return;
        }

        Vector3 targetDirection = _target.position - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }

    private void UpdateCheckers() {
        _isForwardLava = LavaCheck(forwardChecker);
        _isBackwardLava = LavaCheck(backwardChecker);
        _isLeftLava = LavaCheck(leftChecker);
        _isRightLava = LavaCheck(rightChecker);
    }

    private bool LavaCheck(GameObject checker) {
        List<Collider2D> collisions = new List<Collider2D>(Physics2D.OverlapCircleAll(checker.transform.position, .25f));

        bool isLava = collisions.Exists(col => {
            LavaController lava = col.GetComponent<LavaController>();

            return !!lava;
        });

        /*
        SpriteRenderer sprite = checker.GetComponentInChildren<SpriteRenderer>();
        if (isLava) {
            sprite.color = Color.red;
        } else {
            sprite.color = Color.white;
        }*/

        return isLava;
    }
}
