using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("TetrisLevel1");
    }

    public void GameMenu()
    {
        SceneManager.LoadScene("reklama");
    }
}
