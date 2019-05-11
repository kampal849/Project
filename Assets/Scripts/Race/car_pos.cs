using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_pos : MonoBehaviour
{


    public GameObject[] car;
    public float maxx = 2.52f;
    public float minx = -2.47f;
    float timer;
    float speedtime = 6f;
    public float delaytimer = 1.3f;
    float delay = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        timer = delaytimer;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
       
        if (timer <= 0)
        {
            
            Vector3 car_position = new Vector3(Random.Range(minx, maxx), transform.position.y, transform.position.z);
            int carnumber = Random.Range(0,5);
            Instantiate(car[carnumber], car_position, transform.rotation);

          if(speedtime>0)
            { 
                timer = delaytimer;
                speedtime = speedtime -= timer;

            }
            else
            {
                delay = delaytimer - 0.2f;
                timer = delaytimer;
                speedtime = 6f;
                delaytimer = delay;

                if (delaytimer>=0.0 && delaytimer <=0.6)
                {
                    delaytimer = 0.4f;
                    timer = 0.4f;
                }
            }

        }

        if (transform.position.y <= -7f)
        {
            Destroy(gameObject);

        }
    }


}




