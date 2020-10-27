using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucisseBehaviour : MonoBehaviour
{
    public bool onFire = false;
    public float colorRGB = 1.0f;
    public Color saucisseColor;

    int frameCounter = 0;
    float colorStep = 0.05f;
    public string saucisseState = "notCooked";

    // Update is called once per frame
    void Update()
    {
        if (onFire)
        {
            frameCounter++;
            if (frameCounter > 60)
            {
                if (colorRGB > 0.6f)    //saucisse en cuisson
                {
                    colorRGB -= colorStep;
                    frameCounter = 0;
                    saucisseState = "inCooking";
                }
                else if (colorRGB > 0.4f && colorRGB <= 0.6f)   //saucisse cuite
                {
                    if(frameCounter > 800)
                    {
                        colorRGB -= colorStep;
                        frameCounter = 540;
                    }
                    saucisseState = "Good";
                }
                else    // saucisse brulée
                {
                    frameCounter = 0;
                    saucisseState = "Burned";
                }
            }
        }

        saucisseColor = new Color(colorRGB, colorRGB, colorRGB);
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", saucisseColor);
    }
}
