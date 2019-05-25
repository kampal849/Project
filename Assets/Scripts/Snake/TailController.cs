using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour 
{
	public Vector3 destination;//{ get; set; }
	SnakeGameController gameController;
	PlayerController playerController;

	void Start()
	{
		gameController = GameObject.Find ("GameController").GetComponent<SnakeGameController> ();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		playerController.InPosition += Routine;
	}

	void Routine()
	{
		StartCoroutine (Move());
	}

	IEnumerator Move()
	{
		while(transform.position != destination)
		{
			transform.position = Vector3.MoveTowards (transform.position, destination, gameController.speed * Time.deltaTime);
			yield return null;
		}
	}

	public void Enable()
	{
		GetComponent<CircleCollider2D> ().enabled = true;
		GetComponentInChildren<SpriteRenderer> ().enabled = true;
	}

	public void Delete()
	{
		playerController.InPosition -= Routine;
		Destroy(this.gameObject);
	}
}
