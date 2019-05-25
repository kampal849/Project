using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainCanvasController : MonoBehaviour
{
    GameType game;
    Canvas mainCanvas;

    public Camera mainCamera;
    public GameObject prevButt, nextButt, optionsButt, playButt, locked, optionsCanvas;
    public TextMeshProUGUI totalScoreText, currentEnergyText;
    public Slider audioSlider;

    private void Start()
    {
        Vault.Open();
        if(Vault.energyTimer == null)
            Vault.energyTimer = StartCoroutine(AddEnergy());
        mainCanvas = GetComponent<Canvas>();
        audioSlider.value = AudioListener.volume = Vault.playerSettings.audioVolume;
        game = GameType.Tetris;
        CheckButts();
        totalScoreText.text = Vault.GetScore().ToString();
        currentEnergyText.text = Vault.energy.ToString();
    }

    public IEnumerator AddEnergy()
    {
        yield return new WaitForSeconds(60f);
        if (Vault.energy < Vault.ENERGY_MAX)
        {
            Vault.energy += 1;
            currentEnergyText.text = Vault.energy.ToString();
        }

        yield return StartCoroutine(AddEnergy());
    }

    //obsługa cheatów(numpad+: +100 do totala, nunmpad-: -100 do totala)
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            Vault.CheatAddPoints();
            totalScoreText.text = Vault.GetScore().ToString();
        }
        if(Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            Vault.CheatSubPoints();
            totalScoreText.text = Vault.GetScore().ToString();
        }
    }

    public void NextButtonClick()
    {
        game++;
        StartCoroutine(MoveCamera(mainCamera.transform.position + Vector3.right*12));
    }

    public void PrevButtonClick()
    {
        game--;
        StartCoroutine(MoveCamera(mainCamera.transform.position + Vector3.left*12));
    }

    IEnumerator MoveCamera(Vector3 dest)
	{
        mainCanvas.enabled = false;
		while(mainCamera.transform.position != dest)
		{
			mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, dest, 30 * Time.deltaTime);
			yield return null;
		}
        mainCanvas.enabled = true;
        CheckButts();
	}

    void CheckButts()
    {
        bool unlocked = Vault.isUnlocked(game);

        playButt.gameObject.SetActive(unlocked);
        locked.gameObject.SetActive(!unlocked);

        prevButt.SetActive(game != GameType.Tetris);
        nextButt.SetActive(game != GameType.SN);
    }

    public void PlayButtonClick()
    {
        Vault.currentGame = game;
        SceneManager.LoadScene(game.ToString() + "_Menu");
    }

    public void SetAudio()
    {
        AudioListener.volume = audioSlider.value;
    }

    public void EnterOptions()
    {
        optionsCanvas.SetActive(true);
    }

    public void ExitOptions()
    {
        optionsCanvas.SetActive(false);
    }
}
