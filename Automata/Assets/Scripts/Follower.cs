using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	private GameObject host;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		host = gameObject;
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Place the Follower
	public void place(Vector2 pos,float angle){
		body.MovePosition(pos);
		body.MoveRotation(angle);
	}

	public void attach(Resource r){
		r.host.transform.SetParent (host.transform);
		r.host.transform.localPosition = new Vector3 (1,0,0);
		r.host.transform.localEulerAngles = new Vector3 (1, 0, 0);
	}
}
