using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour 
{
	public GameObject foodPrefab;
	public Vector3 foodPosition
	{ 
		get
		{
			return food.transform.position;
		} 
		set
		{
			food.transform.position = value;
		}	
	}

	GameObject food;
	AudioSource audioSource;
	public SnakeGameController gameController;
	PlayerController playerController;

	void Start () 
	{
		playerController = GameObject.Find("Player").GetComponent<PlayerController> ();
		playerController.FoodCollected += FoodEaten;

		audioSource = GetComponent<AudioSource> ();

		SpawnFood (playerController.gameObject.transform.position);
	}

	void FoodEaten(Collider2D item)
	{
		audioSource.Play ();
		SpawnFood (item.transform.position);
		Destroy (item.gameObject);
		gameController.IncreaseSpeed();
	}

	void SpawnFood(Vector3 oldPosition)
	{
		Vector3 newPosition;

		do
		{
			newPosition = RandomSpawnPosition();
		} while(newPosition == oldPosition && Vector3.Distance(newPosition, playerController.gameObject.transform.position) < 10);

		food = Instantiate (foodPrefab, newPosition, Quaternion.identity);
	}

	public void RespawnFood()
	{
		Vector3 pos = food.transform.position;
		Destroy(food);
		SpawnFood(pos);
	}

	public static Vector3 RandomSpawnPosition()
	{
		return new Vector3(Random.Range(-5, 4) + 0.5f, Random.Range(-6, 7) + 0.5f);
	}
}

