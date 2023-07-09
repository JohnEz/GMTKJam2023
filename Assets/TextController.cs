using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public List<string> textList;
    private int _textListPointer = 0;
    private TextMeshProUGUI _textMeshPro;

    private void Awake() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayNextText() {
        _textMeshPro.text = textList[_textListPointer];
        _textListPointer++;
    }
}
