using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class keyBehaviour : MonoBehaviour
{

    public bool isHeld;  //si le smartphone est tenu dans la main du joueur ou pas
    public bool isUsed;  //si la clé a touché le couvercle du coffre

    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
        isUsed = true;
    }

    // Update is called once per frame
    void Update()
    {
        isHeld = GetComponent<Throwable>().GetAttached();

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Lock")
        {
            isUsed = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        
    }
}
