using UnityEngine;

public class CutsceneControls : Controls {
    private CutsceneSkip _cutsceneSkip;

    public override bool ControlsEnabled =>
        base.ControlsEnabled
        && _cutsceneSkip
        && GameManager.Instance.IsInCutscene;

    private void Awake() {
        _cutsceneSkip = GetComponentInChildren<CutsceneSkip>();
    }

    private void Update() {
        if (!ControlsEnabled) {
            return;
        }

        if (_controlScheme.SkipCutscene) {
            _cutsceneSkip.Toggle();
        }
    }
}
