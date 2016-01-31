using UnityEngine;
using System.Collections;

public class Adherent : MonoBehaviour {

	private GameObject host;
	private Follower follower;
	public PathObject pathObject;

	private bool isCarrying = false;

	private int type;

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
        Vector2 point = pathObject.Path.GetPointAt(x);
        float angle = pathObject.Path.GetAngleAt(x);
		float inter = pathObject.Path.GetIntersectionBetween (oldX, x);
		Vector2 interPoint = point + new Vector2 (host.transform.position.x, host.transform.position.y);
		if (inter != -1) {
			if (isCarrying) {
				Resource.Pos = interPoint;
				Debug.Log (Resource.Pos);
				this.detach ();
			} else if (Resource.parent == null && (Resource.Pos - interPoint).magnitude < 10f) {//Failure Radius
				Resource.parent = this.attach ();
			}
		}
		follower.place(point+new Vector2(host.transform.position.x,host.transform.position.y), angle);
	}

	public void setpath(Path p){
		pathObject.setShape (p);
	}

	public Adherent attach(){
		isCarrying = true;
		Resource.host.transform.SetParent (follower.host.transform);
		Resource.host.transform.localPosition = new Vector3 (0,0,0);
		Resource.host.transform.localEulerAngles = new Vector3 (1, 0, 0);
		return this;
	}

	public void detach(){
		isCarrying = false;
		Resource.host.transform.parent = null;
		Resource.parent = null;
	}

	public int getType(){
		return type;
	}

	public enum adTypes{
		Rectangle = 0,
		Circle = 1,
		Diamond = 2,
		Triangle = 3
	}
}
