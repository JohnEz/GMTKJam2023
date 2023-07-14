using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public Animator transition;

    public float transitionTime = .75f;

    public void ChangeScene(string sceneName) {
        StartCoroutine(LoadLevel(sceneName));
    }

    private IEnumerator LoadLevel(string sceneName) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
