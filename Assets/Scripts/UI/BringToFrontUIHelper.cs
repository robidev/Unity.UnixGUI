using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringToFrontUIHelper : MonoBehaviour
{
    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    public void SetColor(Color color)
    {
        if(image != null)
        {
            image.color = color;
        }
    }
}
