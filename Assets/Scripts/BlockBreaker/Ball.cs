using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public gameManager gm;
    public GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOverBool)
        {
            return;     //przestan sie ruszac jak nie ma zyc
        }

        if(!inPlay)
        {
            transform.position = paddle.position;   //Pilka przyjmuje pozycje naszej platformy
        }

        if (Input.GetButtonDown("Jump") && !inPlay)     //gdy naciszniesz spacje(jump = spacja)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);   //szybkosc pily
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bottom"))               //Gdy dotyka dolna granice
        {
            Debug.Log("it is working");
            rb.velocity = Vector2.zero;             //zeby nie wylecialo z platformy
            inPlay = false;       //Przypisz pilke do platformy
            gm.updateLives(-1);  //odejmij jedno zycie gdy padniesz
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("brick"))
        {
            Brick brickScript = collision.gameObject.GetComponent<Brick>(); //wejdz do skryptu Brick.cs i pobierz zmienna 
            if (brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            }
            else
            {
                int randomChance = Random.Range(1, 101);
                if (randomChance < 50)
                {
                    Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
                }

                Transform newExplosion = Instantiate(explosion, collision.transform.position, collision.transform.rotation); //Stworz wybuch w miejscu usuniecia bloku
                Destroy(newExplosion.gameObject, 2.5f);  //usun efekt wybuchu

                gm.updateScore(brickScript.points);  //ile punktow daje cegla

                gm.updateNumberOfBricks(); //upadte liczbe cegiel 

                Destroy(collision.gameObject);  //zniszcz blok(bricka)
            }
            


        }
    }

}
