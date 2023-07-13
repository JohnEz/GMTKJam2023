using System;
using UnityEngine;

public class ControlSchemeManager : Singleton<ControlSchemeManager> {
    [SerializeField]
    private GameObject _defaultControlScheme;

    public ControlScheme ControlScheme { get; private set; }

    public void Awake() {
        SetControlScheme(_defaultControlScheme);
    }

    public void SetControlScheme(GameObject scheme) {
        if (!scheme) {
            throw new Exception("ControlSchemeManager: Attempt to set undefined scheme!");
        }

        ControlScheme = scheme.GetComponent<ControlScheme>();

        if (ControlScheme == null) {
            throw new Exception("ControlSchemeManager: Attempt to set undefined scheme!");
        }
    }
}
