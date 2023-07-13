using TMPro;
using UnityEngine;

public class CutsceneSkip : MonoBehaviour {
    [SerializeField]
    private GameObject _button;

    [SerializeField]
    private TMP_Text _buttonText;

    [SerializeField]
    private GameFlag _introSeen;

    [SerializeField]
    private GameFlag _interludeSeen;

    [SerializeField]
    private float _skipSpeed = 20;

    private bool _isFastForwarding = false;

    private void Awake() {
        _buttonText.text = ">>";
    }

    private void Update() {
        if (!GameManager.Instance.IsInCutscene) {
            Resume();
        }
    }

    public void OnStartIntro() {
        SetVisibility(_introSeen.Value);
    }

    public void OnEndIntro() {
        _introSeen.Value = true;
        SetVisibility(false);
        Resume();
    }

    public void OnStartInterlude() {
        SetVisibility(_interludeSeen.Value);
    }

    public void OnEndInterlude() {
        _interludeSeen.Value = true;
        SetVisibility(false);
        Resume();
    }

    public void Toggle() {
        if (_isFastForwarding) {
            Resume();
        } else {
            SetVisibility(true);
            FastForward();
        }
    }

    private void FastForward() {
        if (!_isFastForwarding) {
            _buttonText.text = ">";
            _isFastForwarding = true;
            Time.timeScale = _skipSpeed;
        }
    }

    private void Resume() {
        if (_isFastForwarding) {
            _buttonText.text = ">>";
            _isFastForwarding = false;
            Time.timeScale = 1;
        }
    }

    private void SetVisibility(bool visible) {
        _button.SetActive(visible);
    }
}
