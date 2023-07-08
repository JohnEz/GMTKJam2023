using UnityEngine;

public class SpawnEffect : Effect {
    [SerializeField]
    private GameObject _objectPrefab;

    protected override void RunEffect(Transform origin) {
        Instantiate(_objectPrefab, origin.position, default);
    }
}
