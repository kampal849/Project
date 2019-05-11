using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().backgroundColor = HSBColor.ToColor(new HSBColor((Time.time/2)%1f, 1, 0.8f));
    }
}
