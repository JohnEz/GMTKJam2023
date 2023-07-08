using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour, ControlScheme {
    [SerializeField]
    private GameObject _defaultControlSchemeProvider;

    private GameObject _controlSchemeProvider;

    private ControlScheme _controlScheme;

    private CharacterStats _characterStats;

    public bool ControlsEnabled => GameManager.Instance.IsGameActive() && !_characterStats.IsDead;

    public bool MoveUp => ControlsEnabled && _controlScheme.MoveUp;

    public bool MoveDown => ControlsEnabled && _controlScheme.MoveDown;

    public bool MoveLeft => ControlsEnabled && _controlScheme.MoveLeft;

    public bool MoveRight => ControlsEnabled && _controlScheme.MoveRight;

    public bool Ability1 => ControlsEnabled && _controlScheme.Ability1;

    public bool Ability2 => ControlsEnabled && _controlScheme.Ability2;

    public bool Ability3 => ControlsEnabled && _controlScheme.Ability3;

    public Vector3 TargetPosition => _controlScheme.TargetPosition;

    public void Awake() {
        _characterStats = GetComponent<CharacterStats>();

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
