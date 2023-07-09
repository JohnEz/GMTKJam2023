using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable() {
        Event.OnEvent += OnEventRaised;
    }

    private void OnDisable() {
        Event.OnEvent -= OnEventRaised;
    }

    public void OnEventRaised() {
        Response.Invoke();
    }
}
