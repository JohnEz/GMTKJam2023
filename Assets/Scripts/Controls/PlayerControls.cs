using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour, ControlScheme {
    [SerializeField]
    private GameObject _defaultControlSchemeProvider;

    private GameObject _controlSchemeProvider;

    private ControlScheme _controlScheme;

    public bool MoveUp => _controlScheme.MoveUp;

    public bool MoveDown => _controlScheme.MoveDown;

    public bool MoveLeft => _controlScheme.MoveLeft;

    public bool MoveRight => _controlScheme.MoveRight;

    public bool Ability1 => _controlScheme.Ability1;

    public bool Ability2 => _controlScheme.Ability2;

    public Vector3 TargetPosition => _controlScheme.TargetPosition;

    public void Awake() {
        SetControlScheme(_defaultControlSchemeProvider);
    }

    public void SetControlScheme(GameObject provider) {
        if (!provider) {
            throw new Exception("PlayerControls: Attempt to set undefined provider!");
        }

        if (_controlSchemeProvider) {
            Destroy(_controlSchemeProvider);
        }

        _controlSchemeProvider = Instantiate(provider, transform);
        _controlScheme = _controlSchemeProvider.GetComponent<ControlScheme>();

        if (_controlScheme == null) {
            throw new Exception("PlayerControls: Control scheme provider did not provide control scheme!");
        }
    }
}
