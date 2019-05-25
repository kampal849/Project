using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public int score, scene;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        score = 0;

        LoadLevel(1);
    }

    public static MainController Get()
    {
        return GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void LoadLevel(int num)
    {
        scene = num;

        if(scene > 8) scene = 1;
        else if(scene < 1) scene = 8;
        SceneManager.LoadScene("MGLevel" + scene.ToString());
    }

    public void ExitGame()
    {
        UpdateScore();
        SceneManager.LoadScene("reklama");
        Destroy(gameObject);
    }

    public void PrevLvl()
    {
        LoadLevel(scene - 1);
    }

    public void NextLvl()
    {
        LoadLevel(scene + 1);
    }

    void UpdateScore()
    {
        Vault.AddTotalScore(score);

        if(score > Vault.GetScore(GameType.MG))
            Vault.SetScore(GameType.MG, score);
    }
}
