using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBonusStart : MonoBehaviour 
{
	public float time;
	public GameObject bonus;
	SnakeMusicController musicController;

	void Start () {
		musicController = GameObject.Find ("GameController/MusicController").GetComponent<SnakeMusicController>();
		musicController.StopMainMusic();
		StartCoroutine (Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds (time);

		bonus.SetActive (true);

		while (bonus != null)
			yield return null;

		if(!musicController.GetComponentInParent<SnakeGameController>().gameover)
			musicController.PlayMainMusic();

		Destroy (this.gameObject);
	}
}
