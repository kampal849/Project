using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailManager : MonoBehaviour 
{
	public class TailPart
	{
		public GameObject gameObject;
		public Vector3 position { get { return gameObject.transform.position; } }
		public TailController controller;

		public TailPart(GameObject gameObject)
		{
			this.gameObject = gameObject;
			controller = gameObject.GetComponent<TailController>();
		}

		public void SendDestination(Vector3 dest)
		{
			controller.destination = dest;
		}
	}

	public GameObject head, tailPrefab;
	public int initialTailLength;

	bool ate;
	int foodCount;
	Vector3 spawnPosition;
	public SnakeGameController gameController;
	PlayerController playerController;
	public List<TailPart> tailParts;

	public int tailSize{ get { return tailParts.Count; } }

	void Start () 
	{
		playerController = head.GetComponent<PlayerController> ();
		playerController.FoodCollected += ExpandIfEaten;
		playerController.InPosition += Routine;

		ate = false;
		foodCount = 0;
		
		switch(playerController.newDirection)
		{
			case PlayerController.Dir.up:
				spawnPosition = Vector3.down;
				break;
			case PlayerController.Dir.down:
				spawnPosition = Vector3.up;
				break;
			case PlayerController.Dir.left:
				spawnPosition = Vector3.right;
				break;
			default:
				spawnPosition = Vector3.left;
				break;
		}

		tailParts = new List<TailPart> ();

		ExpandTail(head);
		for(int i=0; i<initialTailLength; i++) 
		{
			ExpandTail(Instantiate(tailPrefab, tailParts[tailSize - 1].position + spawnPosition, Quaternion.identity));
			EnableTailPart (tailParts [i + 1]);
		}
		ExpandTail(Instantiate(tailPrefab, tailParts[tailSize - 1].position + spawnPosition, Quaternion.identity));	
	}

	void Routine()
	{
		if (ate) 
		{
			EnableTailPart (tailParts [tailSize - 1]);
			Make ();
			ate = false;
		}

		for (int i = 1; i < tailSize; i++)
			tailParts [i].SendDestination (tailParts [i - 1].position);
	}

	void ExpandTail(GameObject item)
	{
		tailParts.Add (new TailPart (item));
	}

	void ExpandIfEaten(Collider2D coll)
	{
		foodCount++;

		if (foodCount >= gameController.tailExpandDelay) 
		{
			foodCount = 0;
			ate = true;
			spawnPosition = tailParts [tailSize - 1].position;
		}
	}

	void EnableTailPart(TailPart part)
	{
		part.controller.Enable();
	}

	public void Make()
	{
		ExpandTail(Instantiate (tailPrefab, spawnPosition, Quaternion.identity));
	}

	public void Delete()
	{
		tailParts [tailSize - 1].controller.Delete ();
		tailParts.RemoveAt(tailSize - 1);
	}
}
