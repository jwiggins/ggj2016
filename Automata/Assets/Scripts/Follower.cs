using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public GameObject host;
	private Rigidbody2D body;

	public GameObject[] spritePrefabs;
	public GameObject[] attachedSpritePrefabs;

	private GameObject sprite;
	private GameObject aSprite;

	private bool flip = false;

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
		sprite = (GameObject)Instantiate (spritePrefabs [i], new Vector3 (0f, 0f, 0f), Quaternion.identity);
		sprite.transform.parent = gameObject.transform;
		sprite.transform.localPosition = Vector3.zero;

		aSprite = (GameObject)Instantiate (attachedSpritePrefabs [i], new Vector3 (0f, 0f, 0f), Quaternion.identity);
		aSprite.transform.parent = gameObject.transform;
		aSprite.transform.localPosition = Vector3.zero;
		aSprite.SetActive(false);
	}

	public void swapSprite(){
		sprite.SetActive (flip);
		aSprite.SetActive (!flip);
		flip = !flip;
	}
}
