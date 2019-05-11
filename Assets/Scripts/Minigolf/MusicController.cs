using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> playlist;
    AudioSource player;
    int song;

    void Start()
    {
        if(playlist.Count < 1)
        {
            Destroy(this);
            return;
        }

        song = Random.Range(0, playlist.Count);
        player = GetComponent<AudioSource>();
        player.clip = playlist[song];
        player.loop = false;

        player.Play();
    }

    void Update()
    {
        if(!player.isPlaying) NextSong();
    }

    public void NextSong()
    {
        song++;
        if(song >= playlist.Count) song = 0;
        player.clip = playlist[song];
        player.Play();
    }
}


