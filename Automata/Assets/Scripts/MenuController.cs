using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {

	float countdownTimer;

	void Start () {
		countdownTimer = 5f;
	}

	void Update () {
		countdownTimer -= Time.deltaTime;
		if (countdownTimer <= 0f)
			SceneManager.LoadScene ("Level1");
	}
}
