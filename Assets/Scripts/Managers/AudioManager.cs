using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField]
    private AudioMixerGroup musicMixer;

    [SerializeField]
    private AudioMixerGroup sfxMixer;

    public void PlaySound(AudioClip clip, Vector3 worldPosition, bool shiftPitch = false) {
        if (!clip) {
            return;
        }

        GameObject soundGameObject = CreateSoundGameObject(clip.name, worldPosition);
        CreateAudioSource(clip, soundGameObject, shiftPitch);
    }

    public void PlaySound(AudioClip clip, Transform parent, bool shiftPitch = false) {
        if (!clip) {
            return;
        }

        GameObject soundGameObject = CreateSoundGameObject(clip.name, parent);
        CreateAudioSource(clip, soundGameObject, shiftPitch);
    }

    private GameObject CreateSoundGameObject(string clipName, Vector3 worldPosition) {
        GameObject soundGameObject = new GameObject("Sound " + clipName);
        soundGameObject.transform.position = worldPosition;

        return soundGameObject;
    }

    private GameObject CreateSoundGameObject(string clipName, Transform parent) {
        GameObject soundGameObject = new GameObject("Sound " + clipName);
        soundGameObject.transform.SetParent(parent, false);

        return soundGameObject;
    }

    private void CreateAudioSource(AudioClip clip, GameObject audioObject, bool shiftPitch = false) {
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.maxDistance = 100f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.loop = false;
        audioSource.outputAudioMixerGroup = sfxMixer;
        audioSource.pitch = shiftPitch ? Random.Range(0.8f, 1.5f) : 1;
        audioSource.Play();

        Destroy(audioObject, audioSource.clip.length);
    }
}
