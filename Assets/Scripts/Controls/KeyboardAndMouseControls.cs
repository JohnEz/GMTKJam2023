using UnityEngine;

public class KeyboardAndMouseControls : MonoBehaviour, ControlScheme {
    public KeyBindings _keyBindings;

    public bool MoveUp => Input.GetKey(_keyBindings.MoveUp);

    public bool MoveDown => Input.GetKey(_keyBindings.MoveDown);

    public bool MoveLeft => Input.GetKey(_keyBindings.MoveLeft);

    public bool MoveRight => Input.GetKey(_keyBindings.MoveRight);

    public bool Ability1 => Input.GetMouseButtonDown(_keyBindings.Ability1);

    public bool Ability2 => Input.GetMouseButtonDown(_keyBindings.Ability2);

    public bool Ability3 => Input.GetKeyDown(_keyBindings.Ability3);

    public bool SkipCutscene => Input.GetKeyDown(_keyBindings.SkipCutscene);

    public Vector3 TargetPosition {
        get {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
    }
}
