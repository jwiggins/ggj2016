using UnityEngine;
using System.Collections;

public class UIDiamond : UIObject {
    public override void toAdherent(Adherent target, Vector2[] points)
    {
        target.setpath(toPath(points));
		target.setType((int)Adherent.adTypes.Diamond);
    }

    public override Path toPath(Vector2[] points)
    {
        return createDiamond(0f, 0f, points[1].x - points[0].x, points[1].y - points[0].y);
    }
    
	private Path createDiamond(float x,float y,float x2,float y2){
		Vector3 v2 = new Vector3 (x2-x,y2-y,0f);
		Vector3 v3 = (Vector3.Cross(v2,new Vector3(0,0,1)));
		v3.Normalize ();
		float length = v2.magnitude;
		Vector3 v2p = v2 * 0.5f + -length * v3 * 0.5f;
		v2 = v2 * 0.5f + length * v3 * 0.5f;
		Vector2[] points = new Vector2[4];
		points[0] = new Vector2(x, y);
		points[1] = new Vector2(v2.x, v2.y);
		points[2] = new Vector2(x2, y2);
		points[3] = new Vector2(v2p.x, v2p.y);
		return new Path(points);
	}
}
