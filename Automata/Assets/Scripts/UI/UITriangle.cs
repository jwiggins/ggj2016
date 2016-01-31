using UnityEngine;
using System.Collections;

public class UITriangle : UIObject {
    public override void toAdherent(Adherent target, Vector2[] points)
    {
        target.setpath(toPath(points));
		target.setType((int)Adherent.adTypes.Triangle);
    }

    public override Path toPath(Vector2[] points)
    {
        return createTriangle(0f, 0f, points[1].x - points[0].x, points[1].y - points[0].y);
    }

	private Path createTriangle(float x,float y,float x2,float y2){
        x = (x2 - x) * 0.333333f + x;
        y = (y2 - y) * 0.333333f + y;
        Vector3 v3 = new Vector3 (x2-x,y2-y,0.0f);
		v3 = Quaternion.Euler (0, 0, 120) * v3;
		Vector2[] points = new Vector2[3];
		points[0] = new Vector2(x+v3.x, y+v3.y);
		points[1] = new Vector2(x2, y2);
		v3 = Quaternion.Euler (0, 0, 120) * v3;
		points[2] = new Vector2(x+v3.x, y+v3.y);
		return new Path(points);
	}
}
