using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubbleController : MonoBehaviour
{
    private SpriteRenderer _backgroundSpriteRenderer;
    private TextMeshPro _textMeshPro;

    public Vector2 padding = new Vector2(4f, 2f);
    public float yAdjust = 0f;

    private void Awake() {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void Setup(string text) {
        ShowChatBubble();

        _textMeshPro.SetText(text);
        _textMeshPro.ForceMeshUpdate();
        Vector2 textSize = _textMeshPro.GetRenderedValues(false);
        Debug.Log(textSize);
        _backgroundSpriteRenderer.size = textSize + padding;

        _backgroundSpriteRenderer.transform.localPosition = new Vector3(_backgroundSpriteRenderer.size.x / 2f - padding.x / 2, yAdjust);
    }

    public void HideChatBubble() {
        _backgroundSpriteRenderer.enabled = false;
        _textMeshPro.enabled = false;
    }

    private void ShowChatBubble() {
        _backgroundSpriteRenderer.enabled = true;
        _textMeshPro.enabled = true;
    }
}
