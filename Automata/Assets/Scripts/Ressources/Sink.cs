using UnityEngine;
using System.Collections;

public class Sink : MonoBehaviour {
	private GameObject host;

	void Start(){
		host = gameObject;
	}

	public void attach(Resource r){
		r.host.transform.SetParent (host.transform);
		r.host.transform.localPosition = new Vector3 (1,0,0);
		r.host.transform.localEulerAngles = new Vector3 (1,0,0);
	}
}
