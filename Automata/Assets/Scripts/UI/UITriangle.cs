using UnityEngine;
using System.Collections;

public class UITriangle : UIObject {

	public override void toAdherent(Adherent target,Vector2[] points){
		target.setpath(createTriangle(0f,0f,points[1].x-points[0].x,points[1].y-points[0].y));
	}

	private Path createTriangle(float x,float y,float x2,float y2){
		Vector2[] points = new Vector2[3];
		points[0] = new Vector2(x, y);
		points[1] = new Vector2(x2, x);
		points[2] = new Vector2(x2, y2);
		return new Path(points);
	}
}
