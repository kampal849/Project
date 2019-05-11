using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Button nextButton, exitButton, retryButton;
    public Text scoreText;
    MainController mainController;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        mainController = MainController.Get();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void ExitGame()
    {
        mainController.ExitGame();
    }

    public void NextLevel()
    {
        mainController.LoadLevel(mainController.scene + 1);
    }

    public void RetryLevel()
    {
        mainController.LoadLevel(mainController.scene);
    }

    public void HitBall()
    {
        gameController.HitBall();
    }
}
