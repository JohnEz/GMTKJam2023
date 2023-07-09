using UnityEngine;

public class Dialog {
    public string text;
    public float secsDuration;
}

public class CharacterChatBubbleController : MonoBehaviour
{
    private ChatBubbleController _chatBubble;

    private void Awake() {
        _chatBubble = transform.Find("ChatBubble").GetComponent<ChatBubbleController>();
    }
    public void Chat(string text, float secsDuration) {
        _chatBubble.Setup(text);
        Invoke("StopChatting", secsDuration);
    }

    private void StopChatting() {
        _chatBubble.HideChatBubble();
    }
}
