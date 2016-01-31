using UnityEngine;
using System.Collections;

public class Adherent : MonoBehaviour {

	private GameObject host;
	private Follower follower;
	public PathObject pathObject;

	private float x = 0;

	// Use this for initialization
	void Awake () {
		host = gameObject;
		follower = transform.GetChild (0).GetComponent<Follower>();
		pathObject = transform.GetChild (1).GetComponent<PathObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float oldX = x;
        x += 4f;
        x %= pathObject.Path.Length;
        if (pathObject.Path.HasIntersectionBetween(oldX, x)) {
            //Debug.Log("cross");
        }
        Vector2 point = pathObject.Path.GetPointAt(x);
        float angle = pathObject.Path.GetAngleAt(x);
		follower.place(point+new Vector2(host.transform.position.x,host.transform.position.y), angle);
	}

	public void setpath(Path p){
		pathObject.setShape (p);
	}
		
}
