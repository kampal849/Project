using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyRotator : MonoBehaviour 
{
	Quaternion rot, toAngle;

	void Start () {
		rot = transform.rotation;
		StartCoroutine (Begin ());
	}

	IEnumerator Begin()
	{
		for (;;) {
			StartCoroutine (RotateMe ());
			yield return new WaitForSeconds (0.3f);
			transform.rotation = rot;
		}
	}

	IEnumerator RotateMe() {
		toAngle = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, -90));
		for(var t = 0f; t < 1; t += Time.deltaTime*2f) {
			transform.rotation = Quaternion.Lerp(rot, toAngle, t);
			yield return null;
		}
	}
}
