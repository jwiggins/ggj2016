using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void CallBackResource();

class CameraAnimator : Object {
	Camera m_Camera;

	List<GameObject> m_CameraFocii;
	float focusTime;
	float tspan;
	int focusIndex;

	bool shifting;
	Vector3 startPos, endPos;

	public CameraAnimator(Camera cam) {
		m_Camera = cam;
		m_CameraFocii = new List<GameObject>(GameObject.FindGameObjectsWithTag("CameraFocus"));
		m_CameraFocii.Sort((x,y) => x.name.CompareTo(y.name));
		focusTime = 5.0f;
		focusIndex = 0;
		shifting = false;
	}

	public void Update (float delta) {
		focusTime -= delta;
		if (focusTime <= 0.0f && focusIndex < m_CameraFocii.Count) {
			startPos = m_Camera.transform.position;
			endPos = m_CameraFocii[focusIndex].transform.position;
			shifting = true;
			m_Camera.transform.position = m_CameraFocii[focusIndex].transform.position;
			focusTime = 5.0f;
			focusIndex += 1;
		}
		else if (focusIndex == m_CameraFocii.Count) {
			shifting = false;
		}
		if (shifting) {
			m_Camera.transform.position = Vector3.Lerp(startPos, endPos, (tspan - focusTime) / tspan);
		}
	}

	public void panToFocii(int i,float timespan){
		startPos = m_Camera.transform.position;
		endPos = m_CameraFocii[focusIndex].transform.position;
		shifting = true;
		m_Camera.transform.position = m_CameraFocii[focusIndex].transform.position;
		focusTime = timespan;
		tspan = timespan;
		focusIndex = i;
		//focusIndex += 1;
	}

	public IEnumerator Pan(CallBackResource callback) {
		while (focusTime > 0.0f) {
			focusTime -= Time.deltaTime;
			m_Camera.transform.position = Vector3.Lerp (startPos, endPos, (tspan - focusTime) / tspan);
			yield return new WaitForEndOfFrame();
		}
		callback ();
	}
}

public class World : MonoBehaviour {

	public static GameObject host;

	Camera m_Camera;
	CameraAnimator m_CamAnimator;

	// All of our spawnable objects
	public GameObject m_ResourcePrefab;
	public GameObject m_AdherentPrefab;
	public GameObject m_ButtonsPrefab;
	public GameObject m_SoundPrefab;

	private List<Adherent> adherentObjects;

	//Scene Data
	public LevelData[] lData;
	public static int currentLevel = 0;
	protected CallBackResource callbackFct;


    public Texture2D[] cursors;

    // Use this for initialization
    void Start () {
		host = gameObject;
		m_Camera = GetComponent<Camera>();
		m_CamAnimator = new CameraAnimator(m_Camera);

		//m_Camera.orthographicSize = Screen.width * 0.25f;
		m_Camera.transform.position = new Vector3(-1130,570,-10f);//new Vector3(Screen.width * 0.5f,Screen.height * 0.5f,-10f);

		Instantiate(m_ButtonsPrefab);
		Instantiate(m_SoundPrefab);
		adherentObjects = new List<Adherent> ();

		lData [currentLevel].Fountain.generateResource ();
		Resource.level = currentLevel;

        Cursor.SetCursor(cursors[0], new Vector2(20, 20), CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR
		// Only include the level skip when working in unity
		if (Input.GetKeyDown("space")) {
			nextLevel ();
		}
		#endif

		//m_CamAnimator.Update(Time.deltaTime);
	}

	public void nextLevel(){
		if (lData.Length == currentLevel + 1) {
			Debug.Log ("You Won!");//TODO:Celebrate
		} else {
			currentLevel++;
			m_CamAnimator.panToFocii (currentLevel, lData [currentLevel].PanTime);
			callbackFct = SetupResource;
			StartCoroutine (m_CamAnimator.Pan (callbackFct));
		}

		// Keep the resource on the right level
		Resource.level = currentLevel;
	}

	public void SetupResource(){
		Resource.host.transform.parent = lData [currentLevel].Fountain.host.transform;
		Resource.host.transform.localPosition = Vector3.zero;
		Resource.host.transform.localEulerAngles = Vector3.zero;
		Resource.host.GetComponent<Resource> ().Respawn ();
	}

	public void obstacleCollision(Obstacle obst, GameObject collider) {
		//Debug.Log(obst.GetType().ToString() + " run into by a " + collider.tag);

		GameObject gamObj = collider.transform.parent.gameObject;
		Adherent addy = (Adherent)(gamObj.GetComponent<Adherent>());

        Remove(addy);
	}
    
	public Adherent Add(Vector2 pos){
		Adherent newAd = ((GameObject)Instantiate (m_AdherentPrefab, new Vector3 (pos.x, pos.y, 0), Quaternion.identity)).GetComponent<Adherent> ();
		newAd.level = currentLevel;

		adherentObjects.Add(newAd);
		return adherentObjects [adherentObjects.Count - 1];
	}

    public void Remove(Adherent adherent) {
        if (adherentObjects.Contains(adherent)) {
            adherentObjects.Remove(adherent);
            Destroy(adherent.gameObject);

            FindIntersections();

            if (adherent.isCarrying) {
                lData[currentLevel].Fountain.generateResource();
            }
        }
    }

    public void FindIntersections() {
        for (int p1 = 0; p1 < adherentObjects.Count; p1++) {
            Vector2 pos1 = adherentObjects[p1].transform.position;
			Path path1 = adherentObjects[p1].pathObject.path;
            path1.ClearIntersections();
            for (int p2 = 0; p2 < adherentObjects.Count; p2++) {
                if (p1 == p2) {
                    continue;
                }
                Vector2 pos2 = adherentObjects[p2].transform.position;
				Path path2 = adherentObjects[p2].pathObject.path;
                path1.FindIntersections(path2, pos1, pos2);
            }
        }
    }

    public Adherent FindNearestAdherent(Vector2 point, float maxDist) {
        Adherent closest = null;
        for (int i = 0; i < adherentObjects.Count; i++) {
            Vector2 pos = adherentObjects[i].transform.position;
			Path path = adherentObjects[i].pathObject.path;
            float dist = path.DistanceToPath(point - pos);
            if (dist < maxDist) {
                closest = adherentObjects[i];
                maxDist = dist;
            }
        }
        return closest;
    }
}
