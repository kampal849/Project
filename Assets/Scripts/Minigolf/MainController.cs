using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public int score, hiscore, scene;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        score = 0;
        hiscore = 0;

        LoadLevel(1);
    }

    public static MainController Get()
    {
        return GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void LoadLevel(int num)
    {
        scene = num;

        if(scene >= SceneManager.sceneCountInBuildSettings) scene = 1;
        else if(scene < 1) scene = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene("MGLevel" + scene.ToString());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PrevLvl()
    {
        LoadLevel(scene - 1);
    }

    public void NextLvl()
    {
        LoadLevel(scene + 1);
    }
}
