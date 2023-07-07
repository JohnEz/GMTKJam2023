using UnityEngine;

public class KeyboardAndMouseControls : ControlScheme {
    public KeyBindings _keyBindings;

    public override bool MoveUp => Input.GetKey(_keyBindings.MoveUp);

    public override bool MoveDown => Input.GetKey(_keyBindings.MoveDown);

    public override bool MoveLeft => Input.GetKey(_keyBindings.MoveLeft);

    public override bool MoveRight => Input.GetKey(_keyBindings.MoveRight);

    public override bool Ability1 => Input.GetKey(_keyBindings.Ability1);

    public override bool Ability2 => Input.GetKey(_keyBindings.Ability2);

    public override Vector3 TargetPosition => Input.mousePosition;
}
