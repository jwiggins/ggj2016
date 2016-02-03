using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour{
	public Vector2 Pos;
	public Adherent parent;
	public int level;

	// Use this for initialization
	void Start () {
		parent = null;
		StartCoroutine ("Animate");
	}

	public void Respawn(Fountain fount) {
		parent = null;
		transform.SetParent(fount.gameObject.transform);
		transform.localPosition = new Vector3(1,0,0);
		transform.localEulerAngles = new Vector3(1,0,0);

		StartCoroutine ("Animate");
	}

	public bool hasParent() {
		return parent != null;
	}

	IEnumerator Animate() {
		for (int i = 25; i > 0; i--) {
			transform.position += transform.up*1.5f;
			yield return new WaitForSeconds(0.01f);
		}
		GetComponent<Collider2D>().enabled = true;
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		switch (other.gameObject.tag) {
		case "Follower":
			if (!hasParent()) {
				parent = (other.gameObject.GetComponentInParent<Adherent>()).attach(this);
			}
			break;
		case "Sink":
			parent.detach(this);
			(other.gameObject.GetComponent<Sink>()).attach(this);
			GetComponent<Collider2D>().enabled = false;
			World wrld = World.host.GetComponent<World>();
			if (other.gameObject.GetComponent<Sink>() == wrld.lData[World.currentLevel].Target) {
				Debug.Log ("Level Finished!");
				wrld.nextLevel();
				//Pos = new Vector2(-2f,-2f);
			}
			break;
		}
	}
}
