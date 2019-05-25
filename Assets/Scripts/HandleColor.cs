using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleColor : MonoBehaviour
{
    Image fill;
    public Slider slider;

    void Start()
    {
        fill = GetComponent<Image>();
    }

    public void ChangeColor()
    {
        fill.color = HSBColor.ToColor(new HSBColor((slider.value/slider.maxValue) * 0.3f, 1, 1));
    }
}
