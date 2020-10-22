using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABCScript : MonoBehaviour
{
    public string letterObject;

    private void OnTriggerEnter(Collider col )
    {
        if (col.tag == letterObject)
        {
            if(letterObject == "Sponge")
            {
                gameObject.GetComponentInParent<LastEnigmaScript>().aIsDone = true;
            }
            else if (letterObject == "Notebook")
            {
                gameObject.GetComponentInParent<LastEnigmaScript>().bIsDone = true;
            }
            else if (letterObject == "BlueGlass")
            {
                gameObject.GetComponentInParent<LastEnigmaScript>().cIsDone = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == letterObject)
        {
            if (letterObject == "Sponge")
            {
                gameObject.GetComponentInParent<LastEnigmaScript>().aIsDone = false;
            }
            else if (letterObject == "Notebook")
            {
                gameObject.GetComponentInParent<LastEnigmaScript>().bIsDone = false;
            }
            else if (letterObject == "BlueGlass")
            {
                gameObject.GetComponentInParent<LastEnigmaScript>().cIsDone = false;
            }
        }
    }
}
