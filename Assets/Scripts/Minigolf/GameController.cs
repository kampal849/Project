using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider angleSlider, powerSlider;
    public Button hitButton;
    public GameObject arrow;
    public Text endText;

    int hitCount;
    Rigidbody2D rBall;
    AngleArrow angleArrow;
    Coroutine movingState;
    MainController mainController;
    AudioSource audioSource;

    void Start()
    {
        mainController = MainController.Get();
        hitCount = 0;
        rBall = GameObject.Find("ball").GetComponent<Rigidbody2D>();
        angleArrow = arrow.GetComponent<AngleArrow>();
        audioSource = GetComponent<AudioSource>();
        endText.gameObject.SetActive(false);
    }

    IEnumerator MoveBall()
    {
        hitButton.interactable = false;
        angleArrow.Active(false);
        hitCount++;

        rBall.AddForce(
            new Vector2(
                Mathf.Cos(angleSlider.value * Mathf.Deg2Rad),
                Mathf.Sin(angleSlider.value * Mathf.Deg2Rad))
            * powerSlider.value);
        yield return new WaitForFixedUpdate();

        while(rBall.velocity != Vector2.zero)
        {
            if(Mathf.Abs(rBall.velocity.x) + Mathf.Abs(rBall.velocity.y) < 0.05f)
                rBall.velocity = Vector2.zero;
            yield return null;
        } 

        hitButton.interactable = true;
        angleArrow.SetArrow();
        angleArrow.Active(true);
    }

    public void EndRound()
    {
        StopCoroutine(movingState);

        hitButton.interactable = false;
        angleArrow.Active(false);
        audioSource.Play();

        endText.text = "Zaliczone!\nUderzeń: " + hitCount.ToString();
        endText.gameObject.SetActive(true);
    }

    public void HitBall()
    {
        movingState = StartCoroutine(MoveBall());
    }
}
