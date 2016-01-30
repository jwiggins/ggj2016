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
        const float thickness = 0.03f;

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
}
