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
    bool gameover;
    public Button[] buttons;
    public Text GameOverText;
    public Text GameOverText2;
    void Start()
    {
        gameover = false;
        score = 0;
        InvokeRepeating ("scoreupdate", 0.5f, 0.5f);
    }





    // Update is called once per frame
    void Update()
    {
        
        scoretext.text = "Score: " + score;
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
        SceneManager.LoadScene("Race_Menu");
    }
}
