using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		GameObject obj = other.gameObject;

		Debug.Log("Triggered by: " + obj.GetType().ToString());
	}

	void OnTriggerExit2D (Collider2D other) {
		GameObject obj = other.gameObject;

		Debug.Log("Stopped triggering with: " + obj.GetType().ToString());
	}


}
