using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		GameObject obj = other.gameObject;

		Debug.Log("Collided with: " + obj.GetType().ToString());
	}

	void OnTriggerExit (Collider other) {
		GameObject obj = other.gameObject;

		Debug.Log("Stopped colliding with: " + obj.GetType().ToString());
	}

}
