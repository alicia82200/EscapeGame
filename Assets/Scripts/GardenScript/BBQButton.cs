using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;

public class BBQButton : MonoBehaviour
{
    public bool fire = false;
    public GameObject fireParticule;

    private bool firstTime = true;
    public bool isTouched = false;
    public bool wasTouched = false;
    private int frameCounter = 0;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        isTouched = GetComponent<Interactable>().isHovering;

        if (fire)
        {
            fireParticule.GetComponent<ParticleSystem>().loop = true;

            if (firstTime)
            {
                firstTime = false;
            }

            fireParticule.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            if (firstTime)
            {
                fireParticule.GetComponent<ParticleSystem>().Stop();
            }
            else
            {
                fireParticule.GetComponent<ParticleSystem>().loop = false;
            }
        }

        if (isTouched && wasTouched == false)
        {
            fire = !fire;
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
    }

    private void OnMouseDown()
    {
        fire = !fire;
    }

}
