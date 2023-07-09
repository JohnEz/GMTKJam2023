using UnityEngine;

public class ScreenShakeEffect : Effect {
    [SerializeField]
    private float _intensity = 2.5f;

    [SerializeField]
    private float _time = .2f;

    protected override void RunEffect(Transform origin) {
        CameraManager.Instance.ShakeCamera(_intensity, _time);
    }
}
