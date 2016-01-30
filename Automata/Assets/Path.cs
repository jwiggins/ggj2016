using UnityEngine;
using System.Collections;

public class Path {
    private Vector2[] points;
    private float[] segmentLengths;
    private float length;

    public Path(Vector2[] points)
    {
        this.points = points;
        segmentLengths = new float[points.Length];
        length = 0;
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 p1 = points[i];
            Vector2 p2 = points[(i+1) % points.Length];
            float segmentLength = Vector2.Distance(p1, p2);
            length += segmentLength;
            segmentLengths[i] = segmentLength;
        }
    }

    public static Path createRect(float x1, float y1, float x2, float y2)
    {
        Vector2[] points = new Vector2[4];
        points[0] = new Vector2(x1, y1);
        points[1] = new Vector2(x2, y1);
        points[2] = new Vector2(x2, y2);
        points[3] = new Vector2(x1, y2);
        return new Path(points);
    }

    public static Path createEllipse(float x, float y, float rx, float ry, int segments)
    {
        Vector2[] points = new Vector2[segments];
        float twoPi = Mathf.PI * 2.0f;
        float angleStep = twoPi / (float)segments;
        float angle = 0;
        for (int i = 0; i < segments; i++)
        {
            points[i] = new Vector2(x + Mathf.Sin(angle) * rx, y + Mathf.Cos(angle) * ry);
            angle += angleStep;
        }

        return new Path(points);
    }

    public Vector2[] Points
    {
        get
        {
            return points;
        }
    }

    public float Length
    {
        get
        {
            return length;
        }
    }


    public Vector2 GetPointAt(float pos)
    {
        int segmentIndex = 0;
        float segmentRatio = 0;
        for (int i = 0; i < points.Length; i++)
        {
            if (pos <= segmentLengths[i])
            {
                segmentIndex = i;
                segmentRatio = pos / segmentLengths[i];
                break;
            }
            pos -= segmentLengths[i];
        }
        Vector2 p1 = points[segmentIndex];
        Vector2 p2 = points[(segmentIndex + 1) % points.Length];
        return (p2 - p1) * segmentRatio + p1;
    }
}
