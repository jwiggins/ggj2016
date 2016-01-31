using UnityEngine;
using System.Collections;

public class UICircle : UIObject {
    public override void toAdherent(Adherent target, Vector2[] points)
    {
        target.setpath(toPath(points));
		target.setType((int)Adherent.adTypes.Circle);
    }

    public override Path toPath(Vector2[] points)
    {
        Vector2 v2 = (points[1] - points[0]);
        float length = v2.magnitude;
        return Path.createEllipse(v2.x * 0.5f, v2.y * 0.5f, length * 0.5f, length * 0.5f, 256);
    }
}
