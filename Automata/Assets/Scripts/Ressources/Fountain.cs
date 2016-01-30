using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour {
	public GameObject resourcePrefab;
	// Use this for initialization

	void Start(){
		generateResource ();
	}
	public void generateResource() {
		Instantiate(resourcePrefab, transform.position, transform.localRotation);
	}
}
