using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slider : MonoBehaviour
{
    Text label;

    void Start()
    {
        label = GetComponent<Text>();
    }

    public void TextUpdate(float value)                     //Aggiornamento valore dello slider, chiamata dall'Inspector degli Slider
    {
        label.text = decimal.Round((decimal)value, 2) + "";
    }
}
