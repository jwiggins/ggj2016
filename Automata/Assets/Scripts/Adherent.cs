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
        x += 2f;
        x %= pathObject.Path.Length;
        Vector2 point = pathObject.Path.GetPointAt(x);
        float angle = pathObject.Path.GetAngleAt(x);
		follower.place(point+new Vector2(host.transform.position.x,host.transform.position.y), angle);
	}

	public void setpath(Path p){
		pathObject.setShape (p);
	}
		
}
