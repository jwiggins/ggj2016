using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	private GameObject host;

	// Use this for initialization
	void Start () {
		host = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Place the Follower
	public void place(Vector2 pos,float angle){
		host.transform.localPosition = new Vector3 (pos.x,pos.y,0);
		host.transform.eulerAngles = new Vector3(0,0,angle);
	}
}
