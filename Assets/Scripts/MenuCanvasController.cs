using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasController : MonoBehaviour
{
    public string sceneName;

    public void PlayButtonClick()
    {
      SceneManager.LoadScene(sceneName);
    }

   public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
