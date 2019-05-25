using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
	public GameObject bonusPrefab, mainCamera, gameController;
	public List<GameObject> bonusList, effectList;
	public float fadeTime, minTime, maxTime;
	public int bonusPoints;

	int rand;
	float bonusLastingTime;
	Vector3 cameraPosition, pickupPosition;
	GameObject pickup;
	PlayerController playerController;
	FoodManager foodManager;

	void Start()
	{
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		foodManager = gameController.GetComponentInChildren<FoodManager> ();

		cameraPosition = mainCamera.transform.position;

		StartCoroutine (Routine ());
	}

	IEnumerator Routine()
	{
		for (; ;) {
			yield return new WaitForSeconds (Random.value * maxTime + minTime);

			pickup = SpawnBonus (ref pickupPosition);

			bonusLastingTime = Time.time + fadeTime;
			while (pickup != null && Time.time<bonusLastingTime)
				yield return null;

			if (pickup == null) 
			{
				rand = Random.Range (0, bonusList.Count);
				StartCoroutine (AnimateBonusPickUp (Instantiate (effectList[rand], pickupPosition, Quaternion.identity)));
				pickup = Instantiate (bonusList[rand]);

				while (pickup != null)
					yield return null;
			} 
			else
				Destroy (pickup);

			if (mainCamera.transform.position != cameraPosition)
				mainCamera.transform.position = cameraPosition;
		}
	}

	GameObject SpawnBonus(ref Vector3 spawnPosition)
	{
		do 
		{
			spawnPosition = FoodManager.RandomSpawnPosition();
		} while(spawnPosition == foodManager.foodPosition || Vector3.Distance(spawnPosition, playerController.gameObject.transform.position) < 50f);

		return Instantiate (bonusPrefab, spawnPosition, Quaternion.identity);
	}

	IEnumerator AnimateBonusPickUp(GameObject item)
	{
		Vector3 target = item.transform.position + new Vector3 (0, 15);

		while (Vector3.Distance (item.transform.position, target) > 0.05f)
		{
			item.transform.position = Vector3.Lerp (item.transform.position, target, 5 * Time.deltaTime);
			yield return null;
		}

		Destroy (item);
	}
}