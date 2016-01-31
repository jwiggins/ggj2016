using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public GameObject host;
	private Rigidbody2D body;

	public GameObject[] spritePrefabs;

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

	public void setSprite(int i){
		GameObject obj = (GameObject)Instantiate (spritePrefabs [i], new Vector3 (0f, 0f, 0f), Quaternion.identity);
		obj.transform.parent = gameObject.transform;
		obj.transform.localPosition = Vector3.zero;
	}
}
