using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	Camera m_Camera;

	// All of our spawnable objects
	public GameObject m_ResourcePrefab;
	public GameObject m_AdherentPrefab;

	// Use this for initialization
	void Start () {
		m_Camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
