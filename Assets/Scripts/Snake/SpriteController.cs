using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour 
{
	bool isAnimated;
	Animator animator;
	SnakeGameController gameController;
	SpriteRenderer spriteRenderer;
	FoodManager foodManager;
	GameObject player;

	public Sprite normalSprite, deadSprite, eatSprite;
	public bool flip {
		get { return spriteRenderer.flipX; }
		set { spriteRenderer.flipX = value; }
	}

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<SnakeGameController>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		foodManager = gameController.GetComponentInChildren<FoodManager> ();

		isAnimated = animator != null;
		player = GameObject.Find("Player");
	}

	void Update()
	{
		if (isAnimated)
			animator.speed = gameController.speed / 4f;

		else if(Vector3.Distance(player.transform.position, foodManager.foodPosition) < 10)
			spriteRenderer.sprite = eatSprite;

		else if(!gameController.gameover) 
			spriteRenderer.sprite = normalSprite;
	}

	public void SetDeadSprite()
	{
		if(isAnimated)
		{
			Destroy(animator);
			isAnimated = false;
		}

		spriteRenderer.sprite = deadSprite;
	}
}
