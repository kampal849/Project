using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackmove : MonoBehaviour
{

    public float speed;
    public GameObject Level;
    public GameObject gameover;
    float time = 8f;
   public int active=0;
    float timer = 1f;
    public int ustaw ;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        ustaw = 0;
        gameover = GameObject.FindWithTag("panel");
    }

    // Update is called once per frame
    void Update()
    {
        if (active == 1)
            timer -=Time.deltaTime;
        
        if(timer<0)
        {
            Level.gameObject.SetActive(false);
            timer = 2f;
            //active = 0;
        }           

        time -= Time.deltaTime;
        offset = new Vector2(0, Time.time * speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;

        if (time <= 0 && !gameover.GetComponent<panel>().gameover)
        {
            Level.gameObject.SetActive(true);
            active = 1;
            speed += 0.2f;
            time = 8f;
            ustaw++;
        }
        

    }
}
