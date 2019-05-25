using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advertisement : MonoBehaviour
{
    public Sprite[] ads;
    public GameObject exitButt;
    public Text timerText;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        exitButt = GameObject.Find("Button");
        exitButt.gameObject.SetActive(false);
        image.sprite = ads[Random.Range(0, ads.Length)];

        StartCoroutine(ShowAd());
    }

    IEnumerator ShowAd()
    {
        for(int i=5; i>0; i--)
        {
            timerText.text = (i.ToString());
            yield return new WaitForSeconds(1f);
        }
        timerText.gameObject.SetActive(false);
        exitButt.gameObject.SetActive(true);
    }

    public void ExitButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Vault.currentGame.ToString() + "_Menu");
    }
}
