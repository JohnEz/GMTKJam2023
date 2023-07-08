using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour {
    [SerializeField]
    private int _repeat = 1;

    [SerializeField]
    private float _repeatPeriodSeconds = 0;

    public virtual void Execute(Transform origin) {
        StartCoroutine(RunEffects(origin));
    }

    private IEnumerator RunEffects(Transform origin) {
        for (int i = 0; i < _repeat; i++) {
            RunEffect(origin);
            yield return new WaitForSeconds(_repeatPeriodSeconds);
        }
    }

    protected abstract void RunEffect(Transform origin);
}
