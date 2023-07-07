using UnityEngine;

public class KeyboardAndMouseControls : ControlScheme {
    public KeyBindings _keyBindings;

    public override bool moveUp => Input.GetKey(_keyBindings.moveUp);

    public override bool moveDown => Input.GetKey(_keyBindings.moveDown);

    public override bool moveLeft => Input.GetKey(_keyBindings.moveLeft);

    public override bool moveRight => Input.GetKey(_keyBindings.moveRight);
}
