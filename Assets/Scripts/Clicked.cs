using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Clicked : MonoBehaviour
{
    public GameObject obj;

    public void whenButtonClicked()
    {
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
            transform.parent.Find("Button").GetComponentInChildren<Text>().text = "Spara!";
        }
        else
        {
            obj.SetActive(true);
            transform.parent.Find("Button").GetComponentInChildren<Text>().text = "Non Sparare!";
        }
    }
}
