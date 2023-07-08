using UnityEngine;

public class Effect : MonoBehaviour {
    protected CharacterStats _originatorStats;

    public virtual void Execute(CharacterStats originatorStats) {
        _originatorStats = originatorStats;
    }
}
