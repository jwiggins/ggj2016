using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

	private GameObject host;
	private PolygonCollider2D collider;
	private LineRenderer lr;

	// Use this for initialization
	void Start () {
		host = gameObject;
		collider = host.GetComponent<PolygonCollider2D> ();
		lr = host.GetComponent<LineRenderer> ();

		Vector2[] debugPoints = new Vector2[4] { new Vector2 (0, 0),
			new Vector2 (1, 0),
			new Vector2 (1, 1),
			new Vector2 (0, 1)
		};
		setCollider(debugPoints);
		setLineRender(debugPoints);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setCollider(Vector2[] points){
		collider.SetPath (0,points);
	}

	void setLineRender(Vector2[] points){
		lr.SetVertexCount(points.Length);
		Vector3[] v3 = new Vector3[points.Length];
		for (int i = 0; i < points.Length; i++) {
			v3 [i] = new Vector3 (points[i].x,points[i].y,0);
		}
		lr.SetPositions (v3);
	}
}
