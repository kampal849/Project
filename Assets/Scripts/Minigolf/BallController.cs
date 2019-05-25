using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    enum Sound{hit, win};

    public List<AudioClip> soundList;
    public float normalDrag, slowDrag;
    GameController gameController;
    Rigidbody2D rBall;
    AudioSource audioSource;

    private void Start() 
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        rBall = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Block"))
        {
            PlaySound(Sound.hit);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            PlaySound(Sound.win);
            gameObject.SetActive(false);
            gameController.EndRound();
        }

        if(other.gameObject.CompareTag("Slow"))
        {
            rBall.drag = slowDrag;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Slow"))
        {
            rBall.drag = normalDrag;
        }
    }

    void PlaySound(Sound s)
    {
        switch(s)
        {
            case Sound.hit:
                audioSource.clip = soundList[0];
                break;
            case Sound.win:
                audioSource.clip = soundList[1];
                break;
            default:
                audioSource.clip = null;
                break;
        }

        audioSource.Play();
    }
}
