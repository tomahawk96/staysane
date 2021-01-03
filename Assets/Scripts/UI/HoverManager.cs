using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    bool mouseOver = false;
    public float brightnessFactor;
    Color color;

    void Start (){
        color = GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }
    private void OnMouseEnter()
    {
        mouseOver = true;
        var hoverColor = color * brightnessFactor;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", hoverColor);
    }


    private void OnMouseExit()

    {
        mouseOver = false;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}
