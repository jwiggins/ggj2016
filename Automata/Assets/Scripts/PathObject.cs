using UnityEngine;
using System.Collections;

public class PathObject : MonoBehaviour {

	private GameObject host;
	private PolygonCollider2D collider;
	private LineRenderer lr;
	private Path path;

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
		path = Path.createRect (0.0f,0.0f,2.0f,2.0f);
		setCollider(path.Points);
		setLineRender(path.Points);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setCollider(Vector2[] points){
		collider.SetPath (0,points);
	}

	void setLineRender(Vector2[] points){
		lr.SetVertexCount(points.Length+1);
		Vector3[] v3 = new Vector3[points.Length+1];
		for (int i = 0; i < points.Length; i++) {
			v3 [i] = new Vector3 (points[i].x,points[i].y,0);
		}
		v3[points.Length] = new Vector3 (points[0].x,points[0].y,0);
		lr.SetPositions (v3);
	}
}
