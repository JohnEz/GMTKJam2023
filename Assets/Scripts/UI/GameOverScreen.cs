using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {
    [SerializeField]
    private TMP_Text _title;

    [SerializeField]
    private TMP_Text _message;

    [SerializeField]
    private TMP_Text _playButtonText;

    public void Show(string title, string message, string playButtonText) {
        if (_title) {
            _title.text = title;
        }

        if (_message) {
            _message.text = message;
        }

        if (_playButtonText) {
            _playButtonText.text = playButtonText;
        }

        gameObject.SetActive(true);
    }
}
