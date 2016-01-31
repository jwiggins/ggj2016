using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour {
	public Material[] materials;

	void Start(){
		((TrailRenderer)gameObject.GetComponent<TrailRenderer> ()).material = 
			materials [transform.parent.GetComponentInParent<Adherent> ().getType ()];
	}
}
