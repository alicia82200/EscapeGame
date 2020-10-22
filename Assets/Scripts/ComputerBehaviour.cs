using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class ComputerBehaviour : MonoBehaviour
{
    public bool isHeld;  //si la boite est tenue dans la main du joueur ou pas
    public bool isLocked = true;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation de quelques variables
        isHeld = false;
        isLocked = true;
        canvas = transform.GetChild(1).gameObject;
        canvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        isHeld = GetComponent<Throwable>().GetAttached();

        // Si on prend l'ordinateur et qu'il est dévérouillé alors on allume l'écran
        if (isHeld && isLocked == false)
        {
            //Activation du canvas 
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

    }
}
