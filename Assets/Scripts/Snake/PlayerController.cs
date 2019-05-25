using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	public enum Dir { up, down, left, right }

	public delegate void FoodHandler (Collider2D collider);
	public delegate void TailHandler ();
	public delegate void HeadHandler (Dir dir);
	public event FoodHandler FoodCollected;
	public event TailHandler InPosition;
	public event HeadHandler Control;

	public Dir oldDirection, newDirection;

	Vector3 destination;

	public SnakeGameController gameController;
	SpriteController spriteController;

	void Start()
	{
		spriteController = GetComponentInChildren<SpriteController>();

		Control += ChangeDirection;
		newDirection = Dir.up;
		
		StartCoroutine (Movement ());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!gameController.gameover) 
		{
			if (other.gameObject.CompareTag ("Wall") || (other.gameObject.CompareTag ("Killer") && gameController.badTouch))
				PlayerDead();
			if (other.gameObject.CompareTag ("Food"))
				FoodCollected (other);
			if (other.gameObject.CompareTag ("Bonus"))
				Destroy (other.gameObject);
		}
	}

	IEnumerator Movement()		//		funkcja ruchu głowy
	{		
		while(!gameController.gameStarted)
			yield return null;

		for (; ; ) {
			InPosition ();		//		czas na przypisanie koordów ruchu dla ogona i ew wydluzenie 
			destination = SetDestination ();	//		przypisanie koordów ruchu dla głowy
			oldDirection = newDirection;
			yield return StartCoroutine (Move (destination, gameController.speed));			//		rozpoczecie ruchu glowy
		}
	}

	IEnumerator Move(Vector3 dest, float speed)
	{
		while(transform.position != dest)
		{
			transform.position = Vector3.MoveTowards (transform.position, dest, speed * Time.deltaTime);
			yield return null;
		}
	}

	Vector3 SetDestination()
	{
		Vector3 dest = Vector3.zero;

		if (newDirection == Dir.up)
			dest = Vector3.up;

		if (newDirection == Dir.down)
			dest = Vector3.down;

		if (newDirection == Dir.left) 
		{
			dest = Vector3.left;
			spriteController.flip = true;
		}

		if (newDirection == Dir.right) 
		{
			dest = Vector3.right;
			spriteController.flip = false;
		}

		return transform.position + dest;
	}

	public void ChangeDirection(Dir direction)
	{
		if (direction == Dir.up && oldDirection != Dir.down) {
			newDirection = Dir.up;
		} else if (direction == Dir.down && oldDirection != Dir.up) {
			newDirection = Dir.down;
		} else if (direction == Dir.left && oldDirection != Dir.right) {
			newDirection = Dir.left;
		} else if (direction == Dir.right && oldDirection != Dir.left) {
			newDirection = Dir.right;
		}
	}

	public void UpButton()
	{
		Control(Dir.up);
	}

	public void DownButton()
	{
		Control(Dir.down);
	}

	public void LeftButton()
	{
		Control(Dir.left);
	}

	public void RightButton()
	{
		Control(Dir.right);
	}

	public void PlayerDead()
	{
		StopAllCoroutines();
		spriteController.SetDeadSprite ();
		gameController.GameOver ();
	}
}
