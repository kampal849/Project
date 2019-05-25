using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrololoScript : MonoBehaviour 
{
	float duration, interval;
	GameObject player;
	SnakeGameController gameController;
	FoodManager foodManager;
	SnakeMusicController musicController;
	
	void Start () 
	{
		gameController = GameObject.Find("GameController").GetComponent<SnakeGameController>();
		foodManager = gameController.gameObject.GetComponentInChildren<FoodManager>();
		musicController = gameController.GetComponentInChildren<SnakeMusicController>();
		player = GameObject.Find("Player");

		duration = Time.time + GetComponent<AudioSource>().clip.length;
		interval = Time.time + 2f;

		musicController.StopMainMusic();
		GetComponent<AudioSource>().Play ();
	}
	
	void Update () 
	{
		if(gameController.gameover)
			Destroy (this.gameObject);

		if (Time.time > duration) 
		{
			musicController.PlayMainMusic();
			Destroy (this.gameObject);
		}

		if(Vector3.Distance(foodManager.foodPosition, player.transform.position) < 11 || Time.time > interval)
		{
			foodManager.RespawnFood();
			interval = Time.time + 2f;
		}
	}
}
