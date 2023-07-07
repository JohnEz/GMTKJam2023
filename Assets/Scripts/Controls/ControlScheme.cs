using UnityEngine;

public abstract class ControlScheme : MonoBehaviour {
    public abstract bool MoveUp { get; }

    public abstract bool MoveDown { get; }

    public abstract bool MoveLeft { get; }

    public abstract bool MoveRight { get; }

    public abstract bool Ability1 { get; }

    public abstract bool Ability2 { get; }

    public abstract Vector3 TargetPosition { get; }
}
