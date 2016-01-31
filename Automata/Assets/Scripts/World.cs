using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	Camera m_Camera;
	GameObject m_Buttons;

	// All of our spawnable objects
	public GameObject m_ResourcePrefab;
	public GameObject m_AdherentPrefab;
	public GameObject m_ButtonsPrefab;

	private List<Adherent> adherentObjects;

	// Use this for initialization
	void Start () {
		m_Camera = GetComponent<Camera>();
		adherentObjects = new List<Adherent> ();
		m_Camera.orthographicSize = Screen.width * 0.25f;
		m_Camera.transform.position = new Vector3(Screen.width * 0.5f,Screen.height * 0.5f,-10f);

		m_Buttons = (GameObject)Instantiate(m_ButtonsPrefab);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void obstacleCollision(Obstacle obst, GameObject collider) {
		Debug.Log(obst.GetType().ToString() + " run into by a " + collider.tag);

		GameObject gamObj = collider.transform.parent.gameObject;
		Adherent addy = (Adherent)(gamObj.GetComponent<Adherent>());

		if (adherentObjects.Contains(addy)) {
			adherentObjects.Remove(addy);
			Destroy(gamObj);
		}

	}

	public Adherent Add(Vector2 pos){
		adherentObjects.Add(((GameObject)Instantiate (m_AdherentPrefab, new Vector3 (pos.x, pos.y, 0), Quaternion.identity)).GetComponent<Adherent> ());
		return adherentObjects [adherentObjects.Count - 1];
	}

    public void FindIntersections() {
        for (int p1 = 0; p1 < adherentObjects.Count; p1++) {
            Vector2 pos1 = adherentObjects[p1].transform.position;
            Path path1 = adherentObjects[p1].pathObject.Path;
            path1.ClearIntersections();
            for (int p2 = 0; p2 < adherentObjects.Count; p2++) {
                if (p1 == p2) {
                    continue;
                }
                Vector2 pos2 = adherentObjects[p2].transform.position;
                Path path2 = adherentObjects[p2].pathObject.Path;
                path1.FindIntersections(path2, pos1, pos2);
            }
        }
    }
}
