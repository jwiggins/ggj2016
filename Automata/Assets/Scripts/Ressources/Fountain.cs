using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour {
	public GameObject resourcePrefab;
	public GameObject host;
	// Use this for initialization

	void Start(){
		host = gameObject;
		//generateResource ();
	}
	public void generateResource() {
		Instantiate(resourcePrefab, transform.position, Quaternion.LookRotation(transform.forward,transform.right));
	}
}
