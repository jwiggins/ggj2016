using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour{
	public GameObject host;
	// Use this for initialization
	void Start () {
		host = gameObject;
		StartCoroutine ("Animate");
	}

	IEnumerator Animate() {
		for (int i = 25; i > 0; i--) {
			transform.position += transform.right*1.5f;
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		switch (other.gameObject.tag) {
		case "Follower":
			(other.gameObject.GetComponent<Follower> ()).attach (this);
			break;
		case "Sink":
			(other.gameObject.GetComponent<Sink> ()).attach (this);
			host.GetComponent<Collider2D> ().enabled = false;
			//TODO:WIN!
			break;
		}
	}
}
