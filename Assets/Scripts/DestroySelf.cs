using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {
    public float lifespan = 5f;

    public List<GameEvent> triggers;

    public void Awake() {
        triggers.ForEach(trigger => trigger.OnEvent += HandleDestroyTrigger);

        if (lifespan >= 0) {
            Destroy(gameObject, lifespan);
        }
    }

    private void HandleDestroyTrigger() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        triggers.ForEach(trigger => {
            trigger.OnEvent -= HandleDestroyTrigger;
        });
    }
}
