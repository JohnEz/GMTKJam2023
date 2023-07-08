using System.Collections;
using UnityEngine;

public class BeamEffect : Effect {

    [SerializeField]
    private GameObject _beamPrefab;

    [SerializeField]
    private AudioClip _attackSound;

    protected override void RunEffect(Transform origin) {
        GameObject beamGo = Instantiate(_beamPrefab, origin);
        Beam beam = beamGo.GetComponent<Beam>();

        beam.Setup(transform);
    }
}
