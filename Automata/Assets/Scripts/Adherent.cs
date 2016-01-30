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
        x += 0.1f;
        x %= pathObject.Path.Length;
        Vector2 point = pathObject.Path.GetPointAt(x);
        float angle = pathObject.Path.GetAngleAt(x);
        follower.place(point, angle);
	}
		
}
