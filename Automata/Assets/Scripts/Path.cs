﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class IntersectionData {
    public float pos;
    public List<WeakReference> adherents;

    private IntersectionData() {
        pos = -1f;
        adherents = new List<WeakReference>();
    }

    public IntersectionData(float position, Adherent ad1, Adherent ad2) {
        pos = position;
        adherents = new List<WeakReference>();
        adherents.Add(new WeakReference(ad1));
        adherents.Add(new WeakReference(ad2));
    }

    static public IntersectionData createEmpty() {
        return new IntersectionData();
    }

    public bool hasData() {
        return pos != -1 && adherents.Count > 0;
    }
}

public class Path {

    private struct SegmentPos {
        public int index;
        public float ratio;
    }

    private Vector2[] points;
    private float[] segmentLengths;
    private float length;

    private List<IntersectionData> intersectionPositions;
    private WeakReference m_Adherent;

    public Path(Vector2[] points) {
        this.points = points;
        m_Adherent = null;
        segmentLengths = new float[points.Length];
        length = 0;
        for (int i = 0; i < points.Length; i++) {
            Vector2 p1 = points[i];
            Vector2 p2 = points[(i + 1) % points.Length];
            float segmentLength = Vector2.Distance(p1, p2);
            length += segmentLength;
            segmentLengths[i] = segmentLength;
        }
    }

    public static Path createRect(float x1, float y1, float x2, float y2) {
        Vector2[] points = new Vector2[4];
        points[0] = new Vector2(x1, y1);
        points[1] = new Vector2(x2, y1);
        points[2] = new Vector2(x2, y2);
        points[3] = new Vector2(x1, y2);
        return new Path(points);
    }

    public static Path createEllipse(float x, float y, float rx, float ry, int segments) {
        Vector2[] points = new Vector2[segments];
        float twoPi = Mathf.PI * 2.0f;
        float angleStep = twoPi / (float)segments;
        float angle = 0;
        for (int i = 0; i < segments; i++) {
            points[i] = new Vector2(x + Mathf.Sin(angle) * rx, y + Mathf.Cos(angle) * ry);
            angle += angleStep;
        }

        return new Path(points);
    }

    public Adherent parentAdherent {
        get {
            return (Adherent)m_Adherent.Target;
        }

        set {
            m_Adherent = new WeakReference(value);
        }
    }

    public Vector2[] Points {
        get {
            return points;
        }
    }

    public float Length {
        get {
            return length;
        }
    }

    private SegmentPos GetSegmentPos(float pos) {
        SegmentPos result = new SegmentPos();
        for (int i = 0; i < points.Length; i++) {
            if (pos <= segmentLengths[i]) {
                result.index = i;
                result.ratio = pos / segmentLengths[i];
                break;
            }
            pos -= segmentLengths[i];
        }
        return result;
    }

    public Vector2 GetPointAt(float pos) {
        SegmentPos segmentPos = GetSegmentPos(pos);
        Vector2 p1 = points[segmentPos.index];
        Vector2 p2 = points[(segmentPos.index + 1) % points.Length];
        return (p2 - p1) * segmentPos.ratio + p1;
    }

    public float GetAngleAt(float pos) {
        SegmentPos segmentPos = GetSegmentPos(pos);
        Vector2 p1 = points[segmentPos.index];
        Vector2 p2 = points[(segmentPos.index + 1) % points.Length];
        Vector2 v = p2 - p1;
        return Mathf.Atan2(v.y, v.x) * 180.0f / Mathf.PI - 90.0f;
    }

    public float GetPositionAt(Vector2 pt) {
        int closeIdx = 0;
        float minDist = 10000f;

        for (int i = 0; i < points.Length; ++i) {
            float dist = (points[i] - pt).magnitude;
            if (dist < minDist) {
                minDist = dist;
                closeIdx = i;
            }
        }

        float pos = 0f;
        for (int i = 0; i < closeIdx; ++i) {
            pos += segmentLengths[i];
        }

        return pos;
    }

    public void ClearIntersections() {
        intersectionPositions = new List<IntersectionData>();
    }

    public void SortIntersections() {
        intersectionPositions.Sort((obj1, obj2) => obj1.pos.CompareTo(obj2.pos));
    }

    public void FindIntersections(Path p, Vector2 offset1, Vector2 offset2) {
        float pos = 0;
        for (int i = 0; i < points.Length; i++) {
            Vector2 p1p1 = points[i] + offset1;
            Vector2 p1p2 = points[(i + 1) % points.Length] + offset1;

            for (int j = 0; j < p.points.Length; j++) {
                Vector2 p2p1 = p.points[j] + offset2;
                Vector2 p2p2 = p.points[(j + 1) % p.points.Length] + offset2;

                float r = lineLineIntersectionRatio(p1p1, p1p2, p2p1, p2p2);
                if (r >= 0 && r <= 1) {
                    IntersectionData data = new IntersectionData(pos + r * segmentLengths[i], this.parentAdherent, p.parentAdherent);
                    intersectionPositions.Add(data);
                }
            }

            pos += segmentLengths[i];
        }
    }

    private float lineLineIntersectionRatio(Vector2 av1, Vector2 av2, Vector2 bv1, Vector2 bv2) {
        float bottom1 = -av1.x * bv1.y + av1.x * bv2.y + av2.x * bv1.y - av2.x * bv2.y +
            bv1.x * av1.y - bv1.x * av2.y - bv2.x * av1.y + bv2.x * av2.y;
        if (bottom1 != 0) {
            var top1 = -av1.x * bv1.y + av1.x * bv2.y + bv1.x * av1.y - bv1.x * bv2.y - bv2.x * av1.y + bv2.x * bv1.y;
            var r1 = top1 / bottom1;
            if (r1 >= 0 && r1 <= 1) {
                var top2 = av1.x * av2.y - av1.x * bv1.y - av2.x * av1.y + av2.x * bv1.y +
                    bv1.x * av1.y - bv1.x * av2.y;
                var bottom2 = -av1.x * bv1.y + av1.x * bv2.y + av2.x * bv1.y - av2.x * bv2.y +
                    bv1.x * av1.y - bv1.x * av2.y - bv2.x * av1.y + bv2.x * av2.y;
                var r2 = top2 / bottom2;
                if (r2 >= 0 && r2 <= 1) {
                    return r1;
                }
            }
        }
        return -1;
    }

    public bool HasIntersectionBetween(float pos1, float pos2) {
        for (int i = 0; i < intersectionPositions.Count; i++) {
            float ipos = intersectionPositions[i].pos;
            if (ipos >= pos1 && (ipos < pos2 || pos2 < pos1)) {
                return true;
            }
        }
        return false;
    }

    public IntersectionData GetIntersectionBetween(float pos1, float pos2) {
        for (int i = 0; i < intersectionPositions.Count; i++) {
            float ipos = intersectionPositions[i].pos;
            if (ipos >= pos1 && (ipos < pos2 || pos2 < pos1)) {
                return intersectionPositions[i];
            }
        }
        return IntersectionData.createEmpty();
    }

    public float DistanceToPath(Vector2 point) {
        float closest = float.MaxValue;
        for (int i = 0; i < points.Length; i++) {
            float distSqr = DistanceToLineSegmentSqr(point, i);
            if (distSqr < closest) {
                closest = distSqr;
            }
        }
        return Mathf.Sqrt(closest);
    }

    private float DistanceToLineSegmentSqr(Vector2 point, int segement) {
        Vector2 p1 = points[segement];
        Vector2 p2 = points[(segement + 1) % points.Length];

        float t = Vector2.Dot(point - p1, p2 - p1) / (p2 - p1).sqrMagnitude;
        if (t < 0) {
            return (point - p1).sqrMagnitude;
        }
        else  if (t > 1) {
            return (point - p2).sqrMagnitude;
        }

        Vector2 nearest = p1 + (p2 - p1) * t;

        return (point - nearest).sqrMagnitude;
    }
}
