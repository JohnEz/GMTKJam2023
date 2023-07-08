using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager> {

    [SerializeField]
    private CinemachineVirtualCamera _gameCamera;

    private float _shakeTimer;

    private void Awake() {
    }

    public void ShakeCamera(float intensity, float time) {
        SetShakeIntensity(intensity);
        _shakeTimer = time;
    }

    private void Update() {
        if (_shakeTimer <= 0) {
            return;
        }

        _shakeTimer -= Time.deltaTime;

        if (_shakeTimer <= 0f) {
            SetShakeIntensity(0f);
        }
    }

    private void SetShakeIntensity(float intensity) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _gameCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
}
