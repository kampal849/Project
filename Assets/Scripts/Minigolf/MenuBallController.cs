using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBallController : MonoBehaviour
{
    private void Start() 
    {
        float angle = Random.Range(0, 2*Mathf.PI);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 20);
    }
}
