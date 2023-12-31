using TMPro;
using UnityEngine;

public class ChatBubbleController : MonoBehaviour {
    private SpriteRenderer _backgroundSpriteRenderer;
    private SpriteRenderer _arrowSpriteRenderer;
    private TextMeshPro _textMeshPro;

    private Vector3 originalTextMeshPosition;

    public Vector2 padding = new Vector2(4f, 2f);
    public float yAdjust = 0f;

    private void Awake() {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _arrowSpriteRenderer = transform.Find("Arrow").GetComponent<SpriteRenderer>();
        _textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

        HideChatBubble();

        originalTextMeshPosition = _textMeshPro.transform.localPosition;
    }

    public void Setup(string text) {
        ShowChatBubble();

        _textMeshPro.SetText(text);
        _textMeshPro.ForceMeshUpdate();
        Vector2 textSize = _textMeshPro.GetRenderedValues(false);
        _backgroundSpriteRenderer.size = textSize + padding;

        _backgroundSpriteRenderer.transform.localPosition = new Vector3(_backgroundSpriteRenderer.size.x / 2f - padding.x / 2 - textSize.x / 2, yAdjust);
        _textMeshPro.transform.localPosition = originalTextMeshPosition + new Vector3(-textSize.x / 2, 0, 0);
    }

    public void HideChatBubble() {
        _backgroundSpriteRenderer.enabled = false;
        _textMeshPro.enabled = false;
        _arrowSpriteRenderer.enabled = false;
    }

    private void ShowChatBubble() {
        _backgroundSpriteRenderer.enabled = true;
        _textMeshPro.enabled = true;
        _arrowSpriteRenderer.enabled = true;
    }
}
