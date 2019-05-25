using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeWeed : MonoBehaviour 
{
	float duration, speed;
	SnakeGameController gameController;
	PlayerController playerController;

	void Start () 
	{
		gameController = GameObject.Find ("GameController").GetComponent<SnakeGameController> ();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();

		duration = Time.time + 10.4f;

		speed = gameController.speed;
		gameController.speed = speed * 0.75f;

		playerController.Control -= playerController.ChangeDirection;
		playerController.Control += this.ChangeDirection;
	}
	

	void Update () 
	{
		if(gameController.gameover)
			Destroy (this.gameObject);

		if (Time.time > duration) {
			gameController.speed = speed;
			playerController.Control -= this.ChangeDirection;
			playerController.Control += playerController.ChangeDirection;
			Destroy (this.gameObject);
		}
	}

	public void ChangeDirection(PlayerController.Dir dir)
	{
		if (dir == PlayerController.Dir.up && playerController.oldDirection != PlayerController.Dir.up) {
			playerController.newDirection = PlayerController.Dir.down;
		} else if (dir == PlayerController.Dir.down && playerController.oldDirection != PlayerController.Dir.down) {
			playerController.newDirection = PlayerController.Dir.up;
		} else if (dir == PlayerController.Dir.left && playerController.oldDirection != PlayerController.Dir.left) {
			playerController.newDirection = PlayerController.Dir.right;
		} else if (dir == PlayerController.Dir.right && playerController.oldDirection != PlayerController.Dir.right) {
			playerController.newDirection = PlayerController.Dir.left;
		}
	}
}
