public class PlayerAbilityControls : PlayerControls {
    private Abilities _abilities;

    public override bool ControlsEnabled => base.ControlsEnabled && _abilities;

    protected override void Awake() {
        base.Awake();

        _abilities = GetComponentInChildren<Abilities>();
    }

    private void Update() {
        if (!ControlsEnabled) {
            return;
        }

        int? abilityIndex = _controlScheme.Ability1
            ? 0
            : _controlScheme.Ability2
            ? 1
            : _controlScheme.Ability3
            ? 2
            : null;

        if (abilityIndex != null) {
            _abilities.TryExecute(
                abilityIndex.Value,
                _controlScheme.TargetPosition
            );
        }
    }
}
