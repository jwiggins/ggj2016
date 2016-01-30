using UnityEngine;
using System.Collections;

public class PathObject : MonoBehaviour {

	private GameObject host;
	private PolygonCollider2D collider;
	private Path path, path2;

	// Use this for initialization
	void Awake () {
		host = gameObject;
		collider = host.GetComponent<PolygonCollider2D> ();
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
        const float thickness = 5f;

        Vector3[] vertices = new Vector3[points.Length * 4];
        int[] triangles = new int[points.Length * 6];
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 p1 = points[i];
            Vector2 p2 = points[(i + 1) % points.Length];
            Vector2 dir = p2 - p1;
            dir.Normalize();
            Vector2 norm = new Vector2(dir.y, -dir.x);

            vertices[i * 4] = p1 - dir * thickness - norm * thickness;
            vertices[i * 4 + 1] = p1 - dir * thickness + norm * thickness;
            vertices[i * 4 + 2] = p2 + dir * thickness - norm * thickness;
            vertices[i * 4 + 3] = p2 + dir * thickness + norm * thickness;

            triangles[i * 6] = i * 4;
            triangles[i * 6 + 1] = i * 4 + 2;
            triangles[i * 6 + 2] = i * 4 + 1;
            triangles[i * 6 + 3] = i * 4 + 1;
            triangles[i * 6 + 4] = i * 4 + 2;
            triangles[i * 6 + 5] = i * 4 + 3;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
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
        path2 = p; // don't ask, it fixes a problem where apparently path gets garbage collected in the background
        setCollider ();
		setLineRender ();
	}
}
