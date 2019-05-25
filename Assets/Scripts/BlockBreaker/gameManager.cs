using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public int lives;
    public int score, thisScore;
    public Text livesText;
    public Text scoreText;
    public bool gameOverBool;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;  //tworzenie tekstu(informacji) 

        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateLives(int changeInLives) //funkcja d odejmowania i dodawania zyc
    {
        lives += changeInLives;

        //jezeli bez zyc to nowa scene ~`na pozniej
        if(lives <= 0)
        {
            lives = 0;
            gameOverfunction(); //jezeli nie ma zyc to przejdz do funkcji gameover
        }

        livesText.text = "Lives: " + lives;
    }

    public void updateScore(int points)  //funckja do zminay punktoww
    {
        score += points;  //dodaj punkty
        scoreText.text = "Score: " + score;  //odswiez text
    }

    public void updateNumberOfBricks()
    {
        numberOfBricks--;  //usun cegle ze calej ilosci
        if(numberOfBricks <= 0)  //jezeli nie ma juz cegiel przejdz do gameover(TODO)
        {
            if(currentLevelIndex >= levels.Length - 1)
            {
                gameOverfunction(); //TODO jezeli nie ma zadnych leveli i cegiel to wygral
            }
            else
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevelIndex + 2);
                gameOverBool = true;
                Invoke("loadLevel", 3f); //zaladuj nowy level
            }
            
        }
    }

    void gameOverfunction()
    {
        gameOverBool = true;  //w update dla balla zatrzymuje wszystko

        if(score > Vault.GetScore(GameType.BB)) Vault.SetScore(GameType.BB, score);
        Vault.AddTotalScore(score);

        gameOverPanel.SetActive(true);  //pokaz panel gameover
    }

    void loadLevel() 
    {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], new Vector2(-1,3), Quaternion.identity); //zmien level
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOverBool = false;
        loadLevelPanel.SetActive(false);
    }

    public void playAgain()
    {
        SceneManager.LoadScene("BBLevel1");  //przy nacisnieciu play again wroc do sceny1 (lub zagranej TODO)
    }

    public void Quit()
    {
        SceneManager.LoadScene("reklama");  //( MENU)
    }


}
