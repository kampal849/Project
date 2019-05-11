using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleArrow : MonoBehaviour
{
    public Slider angleSlider;
    public GameObject oBall;

    public float offset = 1;

    void Start()
    {
        SetArrow();
    }

    public void SetArrow()
    {
        transform.position = new Vector3(
            oBall.transform.position.x + Mathf.Cos(angleSlider.value * Mathf.Deg2Rad) * offset,
            oBall.transform.position.y + Mathf.Sin(angleSlider.value * Mathf.Deg2Rad) * offset,
            0);

        transform.rotation = Quaternion.AngleAxis(angleSlider.value, Vector3.forward);
    }

    public void Active(bool b)
    {
        gameObject.SetActive(b);
    }
}
