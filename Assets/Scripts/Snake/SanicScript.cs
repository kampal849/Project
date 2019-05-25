using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanicScript : MonoBehaviour 
{

	float duration, speed;
	SnakeGameController gameController;
	SnakeMusicController musicController;

	void Start () 
	{
		gameController = GameObject.Find("GameController").GetComponent<SnakeGameController> ();
		musicController = gameController.GetComponentInChildren<SnakeMusicController> ();

		duration = Time.time + GetComponent<AudioSource>().clip.length;

		speed = gameController.speed;
		if (speed < 160)
			gameController.speed = 200;
		else
			gameController.speed *= 1.5f;

		musicController.StopMainMusic();
		GetComponent<AudioSource>().Play ();
	}

	void Update () 
	{
		if(gameController.gameover)
			Destroy (this.gameObject);

		if (Time.time > duration) 
		{
			gameController.speed = speed;
			musicController.PlayMainMusic();
			Destroy (this.gameObject);
		}
	}
}
