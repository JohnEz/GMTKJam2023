using UnityEngine;

[CreateAssetMenu(menuName = "Key Bindings")]
public class KeyBindings : ScriptableObject {
    public KeyCode MoveUp;

    public KeyCode MoveDown;

    public KeyCode MoveLeft;

    public KeyCode MoveRight;

    public int Ability1;

    public int Ability2;

    public KeyCode Ability3;

    public KeyCode SkipCutscene;
}
