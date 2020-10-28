using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallHool : MonoBehaviour
{
    private bool firstTime = true;
    public GameObject piece;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Basketball" && firstTime)
        {
            firstTime = false;
            piece.SetActive(true);
        }
    }
}
