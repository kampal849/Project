using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasController : MonoBehaviour
{
    public enum Game{Tetris, Race, MG, BB, Snake}

    int game;
    public Camera mainCamera;
    public Button prevButt, nextButt, exitButt, playButt;

    private void Start()
    {
        game = 0;
        prevButt.gameObject.SetActive(false);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
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
        ActivateButts(false);
		while(mainCamera.transform.position != dest)
		{
			mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, dest, 30 * Time.deltaTime);
			yield return null;
		}
        ActivateButts(true);
        if(game == 0) prevButt.gameObject.SetActive(false);
        if(game == 4) nextButt.gameObject.SetActive(false);
	}

    void ActivateButts(bool b)
    {
        prevButt.gameObject.SetActive(b);
        nextButt.gameObject.SetActive(b);
        exitButt.gameObject.SetActive(b);
        playButt.gameObject.SetActive(b);
    }

    public void PlayButtonClick()
    {
        if(game == 4) return;
        SceneManager.LoadScene(((Game) game).ToString() + "_Menu");
    }
}
