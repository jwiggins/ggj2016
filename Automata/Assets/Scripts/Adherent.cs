using UnityEngine;
using System.Collections;

public class Adherent : MonoBehaviour {

	public Follower follower;
	public PathObject pathObject;

	private float x = 0;

	// Use this for initialization
	void Start () {
		follower = transform.GetChild (0).GetComponent<Follower>();
		pathObject = transform.GetChild (1).GetComponent<PathObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		follower.place (new Vector2(x,0),0);
		x += 0.1f;
	}
		
}
