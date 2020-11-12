using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SwitchScript : MonoBehaviour
{
    // Moving switch variables
    public float switchAngle = 180.0f;
    private Vector3 lightRot;
    private Vector3 nightRot;
    private bool changeSwitch = false;
    public bool isTouched = false;
    public bool wasTouched = false;
    private int frameCounter = 0;

    // Nightmode variables
    public bool nightMode = false;
    private bool changeLight = false;
    public GameObject linkedLight;
    public GameObject nightNotes;

    void Start()
    {
        lightRot = transform.localEulerAngles;
        nightRot = new Vector3(lightRot.x, lightRot.y + switchAngle, lightRot.z);
    }

    // Update is called once per frame
    void Update()
    {
        isTouched = GetComponent<Interactable>().isHovering;

        if (isTouched && wasTouched == false)
        {
            changeSwitch = !changeSwitch;
            changeLight = true;
            wasTouched = true;
        }
        else if (wasTouched)
        {
            frameCounter++;
            if (frameCounter == 200)
            {
                wasTouched = false;
                frameCounter = 0;
            }
        }

        if (changeSwitch)
        {
            transform.localEulerAngles = nightRot;
            nightMode = true;
        }
        else
        {
            transform.localEulerAngles = lightRot;
            nightMode = false;
        }

        if(changeLight)
        {
            changeLight = false;
            linkedLight.GetComponent<OnOffLight>().lightOff = nightMode;
            nightNotes.SetActive(nightMode);
        }
    }

    // Debug with mouse
    void OnMouseDown()
    {
        changeSwitch = !changeSwitch;
        changeLight = true;
    }

}
