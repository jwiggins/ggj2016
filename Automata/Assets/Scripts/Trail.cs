using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour {
    public Material[] materials;

    void Start() {
        TrailRenderer renderer = (TrailRenderer)gameObject.GetComponent<TrailRenderer>();
        renderer.material = materials[transform.parent.GetComponentInParent<Adherent>().getType()];
    }
}
