using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider powerSlider;
    public Button hitButton;
    public GameObject arrow;
    public Text endText;

    public int finishPoints, timePoints, bonusPoints, finishHits, minHits;
    public float finishTime;

    int hitCount;
    float roundStartTime;
    Rigidbody2D rBall;
    AngleArrow angleArrow;
    Coroutine movingState;
    MainController mainController;

    void Start()
    {
        mainController = MainController.Get();
        hitCount = 0;
        roundStartTime = Time.time;
        rBall = GameObject.Find("ball").GetComponent<Rigidbody2D>();
        angleArrow = arrow.GetComponent<AngleArrow>();
        endText.gameObject.SetActive(false);
    }
    
    public void HitBall()
    {
        movingState = StartCoroutine(MoveBall());
    }

    IEnumerator MoveBall()
    {
        hitButton.interactable = false;
        angleArrow.Active(false);
        hitCount++;

        rBall.AddForce(
            new Vector2(
                Mathf.Cos(angleArrow.azimuthRad),
                Mathf.Sin(angleArrow.azimuthRad)
            ) 
            * powerSlider.value
        );

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

        CountPoints();

        hitButton.interactable = false;
        angleArrow.Active(false);

        endText.text = "Zaliczone!\nUderzeń: " + hitCount.ToString();
        endText.gameObject.SetActive(true);
    }

    void CountPoints()
    {
        int points = 0;

        if(Time.time - roundStartTime <= finishTime) 
            points += timePoints;
        if(hitCount < minHits) 
            points += bonusPoints;
        if(hitCount <= finishHits) 
            points += finishPoints;
        else switch(hitCount - finishHits)
        {
            case 1:
                points += (int) (finishPoints/1.5f);
                break;
            case 2:
                points += finishPoints/3;
                break;
            case 3:
                points += finishPoints/5;
                break;
            default:
                points += finishPoints/10;
                break;
        }

        mainController.score += points;
    }
}
