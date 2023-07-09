using TMPro;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager> {
    [SerializeField]
    private GameObject _gameOverScreen;

    [SerializeField]
    private TMP_Text _gameOverTitle;

    [SerializeField]
    private TMP_Text _gameOverMessage;

    public void ShowGameOver(string title, string message) {
        if (_gameOverTitle) {
            _gameOverTitle.text = title;

            if (_gameOverMessage) {
                _gameOverMessage.text = message;
            }
        }

        _gameOverScreen.SetActive(true);
    }
}
