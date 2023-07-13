using UnityEngine;

public interface ControlScheme {
    bool MoveUp { get; }

    bool MoveDown { get; }

    bool MoveLeft { get; }

    bool MoveRight { get; }

    bool Ability1 { get; }

    bool Ability2 { get; }

    bool Ability3 { get; }

    bool SkipCutscene { get; }

    Vector3 TargetPosition { get; }
}
