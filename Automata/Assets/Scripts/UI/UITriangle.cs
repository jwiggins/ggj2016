using UnityEngine;
using System.Collections;

public class UITriangle : UIObject {
    public override void toAdherent(Adherent target, Vector2[] points) {
        target.setPath(toPath(points));
        target.setType((int)Adherent.adTypes.Triangle);
    }

    public override Path toPath(Vector2[] points) {
        return createTriangle(0f, 0f, points[1].x - points[0].x, points[1].y - points[0].y);
    }

    private Path createTriangle(float x, float y, float x2, float y2) {
        Vector2[] points = new Vector2[3];
        Vector2 mid = new Vector2((x + x2)/2f, (y + y2)/2f);
        Vector3 ray = new Vector3(x2 - x, y2 - y, 0.0f);

        points[0] = new Vector2(x2, y2);
        points[1] = new Vector2(x, y);
        float length = Vector2.Distance(points[0], points[1]) * Mathf.Sqrt(3f) / 2f;

        ray = (Quaternion.Euler(0, 0, 90) * ray).normalized * length;
        points[2] = mid + new Vector2(ray.x, ray.y);

        return new Path(points);
    }
}
