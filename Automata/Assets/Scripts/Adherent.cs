using UnityEngine;
using System.Collections;

public class Adherent : MonoBehaviour {

	private GameObject host;
	private Follower follower;
	public PathObject pathObject;

	public bool isCarrying = false;
	public int level;

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
        x %= pathObject.path.Length;
		Vector2 point = pathObject.path.GetPointAt(x);
		float angle = pathObject.path.GetAngleAt(x);
		float inter = pathObject.path.GetIntersectionBetween (oldX, x);
		Vector2 interPoint = point + new Vector2 (host.transform.position.x, host.transform.position.y);
		if (inter != -1) {
			if (isCarrying) {
				Resource.Pos = interPoint;
				this.detach ();
			} else if (Resource.level == this.level &&
					   Resource.parent == null &&
					   (Resource.Pos - interPoint).magnitude < 10f) {//Failure Radius
				Resource.parent = this.attach ();
			}
		}
		follower.place(point+new Vector2(host.transform.position.x,host.transform.position.y), angle);
	}

	public void setpath(Path p){
		pathObject.setShape (p);
	}

	public Adherent attach(){
		follower.swapSprite ();
		isCarrying = true;
		Resource.host.transform.SetParent (follower.host.transform);
		Resource.host.transform.localPosition = new Vector3 (0,0,0);
		Resource.host.transform.localEulerAngles = new Vector3 (1, 0, 0);
		return this;
	}

	public void detach(){
		follower.swapSprite ();
		isCarrying = false;
		Resource.host.transform.parent = null;
		Resource.parent = null;
	}

	public int getType(){
		return type;
	}

	public void setType(int t){
		type = t;
		follower.setSprite (type);

        Renderer rend = pathObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Unlit/Color");
        if (type == 0) {
            rend.material.SetColor("_Color", new Color(2f / 255f, 184f / 255f, 162f / 255f));
        } if (type == 1) {
            rend.material.SetColor("_Color", new Color(234f / 255f, 108f / 255f, 94f / 255f));
        } if (type == 2) {
            rend.material.SetColor("_Color", new Color(146f / 255f, 113f / 255f, 159f / 255f));
        } if (type == 3) {
            rend.material.SetColor("_Color", new Color(221f / 255f, 181f / 255f, 16f / 255f));
        }
    }

	public enum adTypes{
		Rectangle = 0,
		Circle = 1,
		Diamond = 2,
		Triangle = 3
	}
}
