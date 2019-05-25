using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBackground : MonoBehaviour
{
    public GameObject sprite1, sprite2;
    Transform pos1, pos2;
    bool b;

    private void Start()
    {
        pos1 = sprite1.transform;
        pos2 = sprite2.transform;
        b = true;
    }

    void Update()
    {
        
    }


}
