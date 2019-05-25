using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantTouchScript : MonoBehaviour
{
	float duration;
	SnakeGameController gameController;
	SnakeMusicController musicController;

	void Start () 
	{
		gameController = GameObject.Find("GameController").GetComponent<SnakeGameController>();
		musicController = gameController.GetComponentInChildren<SnakeMusicController>();

		duration = Time.time + GetComponent<AudioSource>().clip.length;
		gameController.badTouch = false;

		musicController.StopMainMusic();
		
		GetComponent<AudioSource>().Play ();
	}

	void Update () 
	{
		if(gameController.gameover)
			Destroy (this.gameObject);
		
		if (Time.time > duration) 
		{
			gameController.badTouch = true;
			musicController.PlayMainMusic();
			Destroy (this.gameObject);
		}
	}
}
