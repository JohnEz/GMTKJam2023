using UnityEngine;

public class PlayerAction : MonoBehaviour {
    private ControlScheme _controlScheme;

    public void Awake() {
        _controlScheme = GetComponent<ControlScheme>();
    }

    void Update() {
        if (_controlScheme.Ability1) {
            Debug.Log("ABILITY ONE AT ");
            Debug.Log(_controlScheme.TargetPosition);
        } else if (_controlScheme.Ability2) {
            Debug.Log("ABILITY TWO AT ");
            Debug.Log(_controlScheme.TargetPosition);
        }
    }
}
