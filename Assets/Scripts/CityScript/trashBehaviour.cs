﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashBehaviour : MonoBehaviour
{
    public bool canBeUsed = false;
    public GameObject men;

    private void Update()
    {
        canBeUsed = men.GetComponent<menBehaviour>().isHelping;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Garbage" && canBeUsed)
        {
            GameObject.Destroy(other.gameObject);
            men.GetComponent<menBehaviour>().garbageInTrash++;
        }
    }
}
