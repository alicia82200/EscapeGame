﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class GardenDoorBehaviour : MonoBehaviour
{
    private bool isTouch;
    public bool foodEaten;
    public GameObject gardenLockObject;

    // Start is called before the first frame update
    void Start()
    {
        isTouch = false;
        foodEaten = false;
        gameObject.GetComponent<Interactable>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gardenLockObject.GetComponent<gardenLock>().isLocked)  // si la serrure est dévérouillée
        {
            gameObject.GetComponent<Interactable>().enabled = true;
        }

        isTouch = GetComponent<Interactable>().isHovering;

        if (isTouch)
        {
            if(foodEaten)
            {
                GameObject.FindGameObjectWithTag("MainCanva").GetComponent<TextDisplay>().EndGame();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                GameObject.FindGameObjectWithTag("MainCanva").GetComponent<TextDisplay>().HungryText();
            }
        }
    }
}
