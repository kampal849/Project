using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update


    public float speed;
    public float maxspeed = 20f;
    public float acceleration = 1.0f;
    public float delaytimer = 1.0f;
    float timer;
    void Start()
    {
        speed = 10f;
        timer = delaytimer;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            speed += acceleration;
            timer = delaytimer;
        }
        if (speed >= 20)
            speed = maxspeed;

        if (transform.position.y <= -7f)
        {
            Destroy(gameObject);

        }

    }
}
