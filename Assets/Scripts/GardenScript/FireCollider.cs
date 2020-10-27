using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour
{
    public GameObject fireButton;
    private bool fireOn;

    private void Update()
    {
        fireOn = fireButton.GetComponent<BBQButton>().fire;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Saucisse" && fireOn)
        {
            other.gameObject.GetComponent<SaucisseBehaviour>().onFire = true;
        }
        else if (other.tag == "Saucisse" && !fireOn)
        {
            other.gameObject.GetComponent<SaucisseBehaviour>().onFire = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Saucisse")
        {
            other.gameObject.GetComponent<SaucisseBehaviour>().onFire = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Saucisse" && fireOn)
        {
            other.gameObject.GetComponent<SaucisseBehaviour>().onFire = true;
        }
        else if (other.tag == "Saucisse" && !fireOn)
        {
            other.gameObject.GetComponent<SaucisseBehaviour>().onFire = false;
        }
    }
}
