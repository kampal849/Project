using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeGameController : MonoBehaviour
{
    public Text endText, exitText, timeText;
	public GameObject gameEnder;
	public bool gameover, gameStarted, badTouch;
	public float speed, acceleration, audioVolume;
	public int tailExpandDelay;
	public GameType gametype;

	SnakeMusicController musicController;

	void Start () 
	{
		musicController = GetComponentInChildren<SnakeMusicController>();

		musicController.PlayMainMusic();

		gameover = false;
		gameStarted = false;
		badTouch = true;

		StartCoroutine(Countdown());
	}

	public void IncreaseSpeed()
	{
		speed += acceleration;
	}

	public void GameOver()
	{
		gameover = true;
		StopAllCoroutines();
		StartCoroutine (EndGame ());
	}

	IEnumerator EndGame()
	{
		musicController.PlayEndMusic ();
		endText.text = "GAME OVER";

		yield return new WaitForSeconds(4);
			UnityEngine.SceneManagement.SceneManager.LoadScene("Snake_Menu");
	}

	IEnumerator Countdown()
	{
		for(int i=3; i>0; i--)
		{
			timeText.text = i.ToString();
			yield return new WaitForSeconds(1);
		}
		timeText.text = "";
		gameStarted = true;
	}

	
}
