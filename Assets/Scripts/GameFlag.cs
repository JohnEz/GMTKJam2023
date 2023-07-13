using UnityEngine;

[CreateAssetMenu]
public class GameFlag : ScriptableObject, ISerializationCallbackReceiver {
    [SerializeField]
    private bool _initialValue = false;

    [HideInInspector]
    public bool Value = false;

    public void OnBeforeSerialize() {
        Value = _initialValue;
    }

    public void OnAfterDeserialize() {
        Value = _initialValue;
    }
}
