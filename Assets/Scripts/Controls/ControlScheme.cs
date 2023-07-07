using UnityEngine;

public abstract class ControlScheme : MonoBehaviour {
    public abstract bool moveUp { get; }

    public abstract bool moveDown { get; }

    public abstract bool moveLeft { get; }

    public abstract bool moveRight { get; }
}
