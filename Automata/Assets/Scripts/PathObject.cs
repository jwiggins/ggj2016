using UnityEngine;
using System.Collections;

public class PathObject : MonoBehaviour {

    private Path m_path;
    private float m_alpha;

    void Start() {
        m_alpha = 1f;
    }

    void setLineRender() {
        setLineRender(m_path.Points);
    }

    void setLineRender(Vector2[] points) {
        const float thickness = 4f;

        Vector3[] vertices = new Vector3[points.Length * 4];
        int[] triangles = new int[points.Length * 6];
        for (int i = 0; i < points.Length; i++) {
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

    public float alpha {
        set {
            if (m_alpha != value) {
                Renderer rend = GetComponent<Renderer>();
                Color c = rend.material.GetColor("_Color");
                rend.material.SetColor("_Color", new Color(c.r, c.g, c.b, value));
                m_alpha = value;
            }
        }
    }

    public Path path {
        get {
            return m_path;
        }
    }

    public void setShape(Path p) {
        m_path = p;
        setLineRender();
    }
}
