using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMenu : MonoBehaviour
{
    public void NormalButton()
    {
        SceneManager.LoadScene("SnakeLevel1");
    }

    public void SpeedButton()
    {
        SceneManager.LoadScene("SnakeLevel2");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
