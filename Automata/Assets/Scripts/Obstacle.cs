using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	static World m_World;

	void Start () {
		GameObject camera = GameObject.FindWithTag("MainCamera");
		m_World = camera.GetComponent<World>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		m_World.obstacleCollision(this, other.gameObject);
	}

}
