using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class USBKeyBehaviour : MonoBehaviour
{
    public bool isHeld;  //si la cle usb est tenue dans la main du joueur ou pas

    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        isHeld = GetComponent<Throwable>().GetAttached();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Computer")
        {
            GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerBehaviour>().isLocked = false;
        }
    }
}
