using UnityEngine;

public class DestroySelf : MonoBehaviour {
    public float lifespan = 5f;

    public void Awake() {
        Destroy(gameObject, lifespan);
    }
}
