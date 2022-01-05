using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slider : MonoBehaviour
{
    Text label;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<Text>();
    }

    // Update is called once per frame
    public void TextUpdate(float value)
    {
        label.text = decimal.Round((decimal)value, 2) + "";
    }
}
