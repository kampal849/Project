using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasController : MonoBehaviour
{
    public string sceneName;
    public TMPro.TextMeshProUGUI scoreText, energyText;
    public GameType game;

    private void Start()
    {
        scoreText.text = Vault.GetScore(game).ToString();
        energyText.gameObject.SetActive(false);
    }

    public void PlayButtonClick()
    {
        if(TakeEnergy())
            SceneManager.LoadScene(sceneName);
    }

    public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public bool TakeEnergy()
    {
        int energyToTake = (int)game + 1;

        if (Vault.energy > energyToTake)
        {
            Vault.energy -= energyToTake;
            return true;
        }
        else
        {
            StartCoroutine(ShowEnergyError());
            return false;
        }
    }

    IEnumerator ShowEnergyError()
    {
        energyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        energyText.gameObject.SetActive(false);
    }
}
