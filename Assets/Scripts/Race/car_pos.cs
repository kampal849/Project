using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_pos : MonoBehaviour
{


    public GameObject[] car;
     GameObject obj;
    public float maxx = 2.52f;
    public float minx = -2.47f;
    public float enemy_speed;
    float timer;
    public float speedtime = 7f;
    public float delaytimer = 1.5f;
    float delay = 0.2f;
   public float time_to_acc=5f;
    // Start is called before the first frame update
    void Start()
    {
        timer = delaytimer;
        

    }

    
    
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        time_to_acc -= Time.deltaTime;
      

        if (timer <= 0)
        {
            


            Vector3 car_position = new Vector3(Random.Range(minx, maxx), transform.position.y, transform.position.z);
            int carnumber = Random.Range(0,5);

          /*  if (time_to_acc <= 0)
            {
                car[carnumber].GetComponent<enemy>().speed ++;
                time_to_acc = 5f;
                enemy_speed = car[carnumber].GetComponent<enemy>().speed;
            }*/


            Instantiate(car[carnumber], car_position, transform.rotation);

          
            if (speedtime>0)
            { 
                timer = delaytimer;
                speedtime = speedtime -= timer;

            }
            else
            {
                delay = delaytimer - 0.2f;
                timer = delaytimer;
                speedtime = 7f;
                delaytimer = delay;

                if (delaytimer>=0.4 && delaytimer <=0.7)
                {
                    delaytimer = 0.6f;
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




