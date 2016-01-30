using UnityEngine;
using System.Collections;

public class Adherent : MonoBehaviour {

	private GameObject host;
	private Follower follower;
	private PathObject pathObject;

	private float x = 0;

	// Use this for initialization
	void Awake () {
		host = gameObject;
		follower = transform.GetChild (0).GetComponent<Follower>();
		pathObject = transform.GetChild (1).GetComponent<PathObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        x += 0.1f;
        x %= pathObject.Path.Length;
        Vector2 point = pathObject.Path.GetPointAt(x);
        float angle = pathObject.Path.GetAngleAt(x);
        follower.place(point, angle);
	}

	public void setpath(Path p){
		pathObject.setShape (p);
	}
		
}
