using UnityEngine;

public class AuraEffect : Effect {

    [SerializeField]
    protected GameObject _auraPrefab;

    protected override void RunEffect(Transform origin) {
        GameObject auraInstance = Instantiate(_auraPrefab, origin);
    }
}
