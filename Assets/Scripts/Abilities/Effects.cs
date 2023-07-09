using UnityEngine;

public class Effects : MonoBehaviour {
    public void Execute(Transform transform) {
        Effect[] effects = GetComponents<Effect>();
        foreach (Effect effect in effects) {
            effect.Execute(transform);
        }
    }
}
