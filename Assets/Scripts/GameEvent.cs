using System;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject {
    public Action OnEvent;

    public void Raise() {
        OnEvent?.Invoke();
    }
}
