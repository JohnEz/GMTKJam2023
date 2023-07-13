public class PlayerControls : Controls {
    protected CharacterStats _characterStats;

    public override bool ControlsEnabled =>
        base.ControlsEnabled
        && GameManager.Instance.IsGameActive
        && !_characterStats.IsDead;

    protected virtual void Awake() {
        _characterStats = GetComponent<CharacterStats>();
    }
}
