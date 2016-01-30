using UnityEngine;
using System.Collections;

public abstract class UIObject {
	public abstract void toAdherent(Adherent a,Vector2[] position);
    public abstract Path toPath(Vector2[] position);
}
