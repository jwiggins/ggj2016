using UnityEngine;
using System.Collections;

public class UIRectangle : UIObject {

	public override void toAdherent(Adherent target,Vector2[] points){
		target.setpath(Path.createRect(0f,0f,points[1].x-points[0].x,points[1].y-points[0].y));
	}
}
