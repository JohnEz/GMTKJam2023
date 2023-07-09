using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCutscene : MonoBehaviour {
    public List<Vector3> startingPath = new List<Vector3>();
    public Vector3 startingPosition = Vector3.zero;
    public List<string> introDialog = new List<string>();

    [SerializeField]
    private float targetPositionTolerance = 0.5f;
    private CharacterMovement _movement;
    private ChatBubbleController _chatBubbleController;
    private Vector3 target = Vector3.zero;
    private int introDialogPointer = 0;

    private void Awake() {
        _movement = GetComponent<CharacterMovement>();
        _chatBubbleController = transform.Find("ChatBubble").GetComponent<ChatBubbleController>();
        _chatBubbleController.HideChatBubble();
    }

    public void SetupIntro() {
        transform.position = startingPosition;
    }

    public void MoveIntoScene() {
        StartCoroutine(MoveAlongPath(startingPath));
    }

    public void PlayNextDialog() {
        DisplayChatBubble(introDialog[introDialogPointer]);
        introDialogPointer++;
    }

    public void HideChatBubble() {
        _chatBubbleController.HideChatBubble();
    }

    private void DisplayChatBubble(string text) {
        _chatBubbleController.Setup(text);
    }

    private IEnumerator MoveToTarget (Vector3 newTarget)
    {
        target = newTarget;
        while (Mathf.Abs(GetDistanceToTarget()) > targetPositionTolerance) {
            MoveTowardsTarget();
            yield return null;
        }
        _movement.MoveDirection = Vector3.zero;
    }

    private IEnumerator MoveAlongPath(List<Vector3> targets) {
        int index = 0;
    
        while(index < startingPath.Count) {
            target = targets[index];
            if (Mathf.Abs(GetDistanceToTarget()) > targetPositionTolerance) {
                MoveTowardsTarget();
            } else {
                index += 1;
            }
            yield return null;
        }

        _movement.MoveDirection = Vector3.zero;
    }

    private void MoveTowardsTarget() {
        _movement.MoveDirection = GetDirectionToTarget();
    }

    private float GetDistanceToTarget() {
        return Vector3.Distance(transform.position, target);
    }

    private Vector3 GetDirectionToTarget() {
        return (target - transform.position).normalized;
    }
}
