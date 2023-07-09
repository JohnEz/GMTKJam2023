using UnityEngine;

public class ScreenShakeEffect : Effect {
    [SerializeField]
    private float _intensity = 2.5f;

    [SerializeField]
    private float _time = .2f;

    protected override void RunEffect(Transform origin) {
        Debug.Log("SCREEN SHAKE");
        Debug.Log(_intensity);
        Debug.Log(_time);
        CameraManager.Instance.ShakeCamera(_intensity, _time);
    }
}
