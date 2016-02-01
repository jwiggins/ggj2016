using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour{
	public static GameObject host;

	public static Vector2 Pos;
	public static Adherent parent;
	public static int level;


	// Use this for initialization
	void Start () {
		Pos = new Vector2(-2f,-2f);
		parent = null;
		host = gameObject;
		StartCoroutine ("Animate");
	}

	public void Respawn(){
		StartCoroutine ("Animate");
	}

	IEnumerator Animate() {
		for (int i = 25; i > 0; i--) {
			transform.position += transform.up*1.5f;
			yield return new WaitForSeconds(0.01f);
		}
		host.GetComponent<Collider2D> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		switch (other.gameObject.tag) {
		case "Follower":
			if (Pos.x == -2f) {
				parent = (other.gameObject.GetComponentInParent<Adherent> ()).attach ();
			}
			break;
		case "Sink":
			parent.detach();
			(other.gameObject.GetComponent<Sink> ()).attach ();
			host.GetComponent<Collider2D> ().enabled = false;
			World wrld = World.host.GetComponent<World> ();
			if (other.gameObject.GetComponent<Sink> () == wrld.lData [World.currentLevel].Target) {
				Debug.Log ("Level Finished!");
				wrld.nextLevel ();
				Pos = new Vector2(-2f,-2f);
			}
			//TODO:WIN!
			break;
		}
	}
}
