using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public float speed;
    public float maxspeed = 20f;
    public float acceleration = 1.0f;
    public float delaytimer = 1.0f;
    float timer;
    float timer_level=8;
    public GameObject level;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        timer = delaytimer;
        level = GameObject.FindWithTag("Quad");
       
    }

    // Update is called once per frame
    void Update()
    {

        if (level.GetComponent<trackmove>().ustaw == 1)
            speed=5.5f;
        if (level.GetComponent<trackmove>().ustaw == 2)
            speed = 6f;



            switch(level.GetComponent<trackmove>().ustaw)
        {
            case 1:
                speed = 5.5f;
                break;
            case 2:
                speed = 6;
                break;
            case 3:
                speed = 6.5f;
                break;
            case 4:
                speed = 7;
                break;
            case 5:
                speed = 7.5f;
                break;
            case 6:
                speed = 8;
                break;
            case 7:
                speed = 8.5f;
                break;
            case 8:
                speed = 9;
                break;
            case 9:
                speed = 9.5f;
                break;
            case 10:
                speed = 10;
                break;







        }



        timer_level -= Time.deltaTime;
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

        if (timer_level <= 0)
        {

            speed++;
            timer_level = 8f;
        }

        if (transform.position.y <= -7f)
        {
            Destroy(gameObject);

        }
    }


}
