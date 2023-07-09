using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager> {

    [SerializeField]
    private CinemachineVirtualCamera _gameCamera;

    [SerializeField]
    private CinemachineVirtualCamera _interludeCamera;

    private float _shakeTimer;

    private void Awake() {
    }

    public void ShakeCamera(float intensity, float time) {
        SetShakeIntensity(_gameCamera, intensity);
        _shakeTimer = time;
    }

    public void ShakeCameraInterlude(float intensity, float time) {
        SetShakeIntensity(_interludeCamera, intensity);
        _shakeTimer = time;
    }

    private void Update() {
        if (_shakeTimer <= 0) {
            return;
        }

        _shakeTimer -= Time.deltaTime;

        if (_shakeTimer <= 0f) {
            SetShakeIntensity(_gameCamera, 0f);
            SetShakeIntensity(_interludeCamera, 0f);
        }
    }

    private void SetShakeIntensity(CinemachineVirtualCamera camera, float intensity) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _gameCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
}
