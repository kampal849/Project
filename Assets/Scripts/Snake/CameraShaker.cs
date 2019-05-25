using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour 
{
	public float intensity;

	Vector3 target;
	GameObject mainCamera;

	void Start () 
	{
		mainCamera = GameObject.Find ("Main Camera");
		target = new Vector3 (Random.value * 2 - 1, Random.value * 2 + 4, -50f);
		StartCoroutine (Shake ());
	}

	IEnumerator Shake()
	{
		for (;;) {
			while (Vector3.Distance (mainCamera.transform.position, target) > 0.2f) {
				mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, target, intensity);
				yield return null;
			}
			target = new Vector3 (Random.value * 4 - 2, Random.value * 4 + 3, -50f);
		}
	}
}
