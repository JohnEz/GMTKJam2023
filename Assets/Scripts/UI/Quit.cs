using UnityEngine;
using UnityEditor;

public class Quit : MonoBehaviour
{
    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
