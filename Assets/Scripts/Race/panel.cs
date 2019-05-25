using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class panel : MonoBehaviour
{
    // Start is called before the first frame update



    public Text scoretext;
    int score;
    public bool gameover;
    public Button[] buttons;
    public Text GameOverText;
    public Text GameOverText2;
    public Text LevelText;
    public GameObject level;

    void Start()
    {
        gameover = false;
        score = 0;
        InvokeRepeating ("scoreupdate", 0.5f, 0.5f);
        level = GameObject.FindWithTag("Quad");
    }





    // Update is called once per frame
    void Update()
    {
        
        scoretext.text = "Score: " + score;
        LevelText.text = "Level: " + (level.GetComponent<trackmove>().ustaw +1);
    }


    void scoreupdate()
    {
        if (!gameover)
            score += 1;

    }


    public void gameoverfunction()
    {
        gameover = true;

        foreach (Button button in buttons)
        {

            button.gameObject.SetActive(true);
        }

        if(score > Vault.GetScore(GameType.Race)) Vault.SetScore(GameType.Race, score);
        Vault.AddTotalScore(score);

        GameOverText2.text = "Total score: " + score;
        GameOverText.gameObject.SetActive(true);
        GameOverText2.gameObject.SetActive(true);

    }
    public void Pause()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void Play()
    {
        SceneManager.LoadScene("RaceLevel1");
    }

    public void Menu()
    {
        SceneManager.LoadScene("reklama");
    }
}
