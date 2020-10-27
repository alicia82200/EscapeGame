using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBQButton : MonoBehaviour
{
    public bool fire = false;
    public GameObject fireParticule;

    private bool firstTime = true;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
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
    }

   /*void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            fire = !fire;
        }
    }*/

    private void OnMouseDown()
    {
        fire = !fire;
    }

}
