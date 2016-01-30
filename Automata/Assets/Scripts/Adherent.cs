using UnityEngine;
using System.Collections;

public class Adherent : MonoBehaviour {

	public Follower follower;
	public Path path;

	private float x = 0;

	// Use this for initialization
	void Start () {
		follower = transform.GetChild (0).GetComponent<Follower>();
		path = transform.GetChild (1).GetComponent<Path>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		follower.place (new Vector2(x,0),0);
		x += 0.1f;
	}
		
}
