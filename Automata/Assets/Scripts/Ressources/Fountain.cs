using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour {
	public GameObject resourcePrefab;
    public GameObject respawnPointPrefab;

	public void generateResource() {
		GameObject prefab = (GameObject)Instantiate(resourcePrefab, transform.position, Quaternion.LookRotation(transform.forward, transform.right));
		Resource res = prefab.GetComponent<Resource>();

		ResourceManager.addResource(res);
		res.Respawn(this);
	}
}
