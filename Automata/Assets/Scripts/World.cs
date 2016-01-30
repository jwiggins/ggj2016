﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	Camera m_Camera;

	// All of our spawnable objects
	public GameObject m_ResourcePrefab;
	public GameObject m_AdherentPrefab;

	private List<Adherent> adherentObjects;

	// Use this for initialization
	void Start () {
		m_Camera = GetComponent<Camera>();
		adherentObjects = new List<Adherent> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Adherent Add(Vector2 pos){
		adherentObjects.Add(((GameObject)Instantiate (m_AdherentPrefab, new Vector3 (pos.x, pos.y, 0), Quaternion.identity)).GetComponent<Adherent> ());
		return adherentObjects [adherentObjects.Count - 1];
	}
}