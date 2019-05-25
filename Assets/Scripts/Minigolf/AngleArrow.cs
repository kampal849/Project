using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleArrow : MonoBehaviour
{
    public GameObject ball;
    public float offset = 1, azimuthRad;
    Vector3 camPos;

    void Start()
    {
        azimuthRad = Mathf.PI / 2;
        SetArrow();
        camPos = GameObject.Find("Main Camera").transform.position;
    }

    private void Update()
    {
        //Android
        if(Input.touchCount > 0)    
        {
            Vector3 touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if(inGameField(touch))
            {
                azimuthRad = Mathf.Atan2(touch.y - ball.transform.position.y, touch.x - ball.transform.position.x);
                SetArrow();
            }
        } 

        //Editor
        if(Input.GetMouseButton(0)) 
        {
            Vector3 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(inGameField(touch))
            {
                azimuthRad = Mathf.Atan2(touch.y - ball.transform.position.y, touch.x - ball.transform.position.x);
                SetArrow();
            }
            Debug.Log(touch);
        }
    }

    public void SetArrow()
    {
        transform.position = new Vector3(
            ball.transform.position.x + Mathf.Cos(azimuthRad) * offset,
            ball.transform.position.y + Mathf.Sin(azimuthRad) * offset,
            0);

        transform.rotation = Quaternion.AngleAxis(azimuthRad * Mathf.Rad2Deg, Vector3.forward);
    }

    public void Active(bool b)
    {
        gameObject.SetActive(b);
    }

    bool inGameField(Vector3 pos)
    {
        if( pos.x > camPos.x - 5.5 &&
            pos.x < camPos.x + 5.5 &&
            pos.y > camPos.y - 6.5 &&
            pos.y < camPos.y + 8.5 ) return true;

        return false;
    }
}
