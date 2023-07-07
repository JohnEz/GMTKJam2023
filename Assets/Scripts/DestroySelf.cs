using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {
    public float lifespan = 5f;

    public void Awake() {
        Destroy(gameObject, lifespan);
    }
}
