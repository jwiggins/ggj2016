using UnityEngine;
using System.Collections;

public class Sink : MonoBehaviour {
	private GameObject host;

	void Start(){
		host = gameObject;
	}

	public void attach(){
		Resource.host.transform.SetParent (host.transform);
		Resource.host.transform.localPosition = new Vector3 (1,0,0);
		Resource.host.transform.localEulerAngles = new Vector3 (1,0,0);
	}
}
