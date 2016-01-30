using UnityEngine;
using System.Collections;

public class PathObject : MonoBehaviour {

	private GameObject host;
	private PolygonCollider2D collider;
	private LineRenderer lr;
	private Path path;

	// Use this for initialization
	void Awake () {
		host = gameObject;
		collider = host.GetComponent<PolygonCollider2D> ();
		lr = host.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setCollider(Vector2[] points){
		collider.SetPath (0,points);
	}

	void setCollider(){
		collider.SetPath (0, path.Points);
	}

	void setLineRender(){
		setLineRender (path.Points);
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

    public Path Path
    {
        get
        {
            return path;
        }
    }
	public void setShape(Path p){
		path = p;
		setCollider ();
		setLineRender ();
	}
}
