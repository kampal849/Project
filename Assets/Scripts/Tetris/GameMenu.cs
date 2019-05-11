﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    
    public Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("highscore",0);
        highScoreText.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("TetrisLevel1");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
