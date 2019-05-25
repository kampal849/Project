using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CutTailScript : MonoBehaviour
{
	int count;
	TailManager tailManager;
	SnakeGameController gameController;

	void Start () 
	{
		gameController = GameObject.Find("GameController").GetComponent<SnakeGameController>();
		tailManager = GameObject.Find ("Player/TailManager").GetComponent<TailManager> ();

		if (tailManager.tailSize < 6)
			Destroy (this.gameObject);
		else Begin ();
	}

	void Begin()
	{
		if (tailManager.tailSize < 11)
			count = 3;
		else if (tailManager.tailSize < 21)
			count = 6;
		else
			count = 11;

		for (; count > 0; count--)
			tailManager.Delete ();
		tailManager.Make ();

		StartCoroutine (End ());
	}

	void Update () 
	{
		if(gameController.gameover)
			Destroy (this.gameObject);
	}

	IEnumerator End()
	{
		yield return new WaitForSeconds (5.4f);
		Destroy (this.gameObject);
	}
}
