using UnityEngine;

public class Controls : MonoBehaviour {
    protected ControlScheme _controlScheme => ControlSchemeManager.Instance.ControlScheme;

    public virtual bool ControlsEnabled => _controlScheme != null;
}
