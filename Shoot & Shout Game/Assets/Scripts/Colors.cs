using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;



public class Colors : MonoBehaviour
{
    // Public variables
    public RawImage centreImage;

    [HideInInspector]
     public int amt;     
      
    // Private variables
    private int colorID = 0;    
    private Color color1 = new Color(255, 255, 255, 255);
    private Color color2 = new Color(255, 0, 0, 255);
    private Color color3 = new Color(0, 255, 0, 255);
    private Color color4 = new Color(0, 0, 255, 255);
    public Color color5 = new Color(0, 0, 0, 255);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorID = Random.Range(1, 6);

        if(colorID == 1)
        {
            centreImage.color = color1;
            amt = 10;
        }
        if(colorID == 2)
        {
            centreImage.color = color2;
            amt = 15;
        }
        if(colorID == 3)
        {
            centreImage.color = color3;
            amt = 5;
        }
        if(colorID == 4)
        {
            centreImage.color = color4;
            amt = 8;
        }
        if(colorID == 5)
        {
            centreImage.color = color5;
            amt = -5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
