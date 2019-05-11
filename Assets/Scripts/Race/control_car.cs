using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class control_car : MonoBehaviour
{


    public float carspeed;
    public float minx;
    public float maxx;
    float timer=0;

    public panel panel;

    Rigidbody2D rb;
    int yes  =0;

    Vector3  position;
    public GameObject m_col;
   PolygonCollider2D m_Collider;
   PolygonCollider2D n_Collider;
    public Text Bonus;
    // Start is called before the first frame update



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
        position = transform.position;
        m_Collider = m_col.GetComponent<PolygonCollider2D>();
        n_Collider = GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        position.x += Input.GetAxis("Horizontal") * carspeed * Time.deltaTime;
        position = transform.position;
        position.x=Mathf.Clamp (position.x,-2.47f, 2.52f);
        position.y = Mathf.Clamp(position.y, -3.47f, 2.5f);
        transform.position = position;

        if(timer<=0)
        {

            yes = 0;
            Bonus.gameObject.SetActive(false);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       

        if (yes == 0)
        {
            if (collision.gameObject.tag == "enemy")
            {
                Destroy(gameObject);
                panel.gameoverfunction();
            }

            if (collision.gameObject.tag == "Bonus")
            {
                n_Collider.enabled = false;
              
                yes = 1;
                timer = 5;
                Bonus.gameObject.SetActive(true);
            }
            n_Collider.enabled = true;
          
        }
        else if (yes ==1 & (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Bonus"))
        {
            
            Destroy(collision.gameObject);
       }



   }






        
       


    




    public void Left()
    {

        rb.velocity = new Vector2(-carspeed, 0);
    }

    public void Right()
    {
        rb.velocity = new Vector2(carspeed, 0);

    }

    public void Up()
    {
        rb.velocity = new Vector2(0, carspeed);

    }

    public void Down()
    {
        rb.velocity = new Vector2(0, -carspeed);

    }


    public void setzero()
    {
        rb.velocity = Vector2.zero;

    }
}
