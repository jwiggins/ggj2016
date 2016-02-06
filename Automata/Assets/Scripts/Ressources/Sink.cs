using UnityEngine;
using System.Collections;

public class Sink : MonoBehaviour {
    public void attach(Resource res) {
        res.gameObject.transform.SetParent(transform);
        res.gameObject.transform.localPosition = new Vector3(1, 0, 0);
        res.gameObject.transform.localEulerAngles = new Vector3(1, 0, 0);
    }
}
