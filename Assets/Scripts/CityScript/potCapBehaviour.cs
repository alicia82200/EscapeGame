using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class potCapBehaviour : MonoBehaviour
{
    private bool isMoovable = false;
    private bool canBeMooved = false;

    public GameObject areaNW;
    public GameObject areaSW;
    public GameObject areaSE;
    public GameObject areaNE;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().enabled = false;
        GetComponent<Throwable>().enabled = false;
        GetComponent<VelocityEstimator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canBeMooved && isMoovable == false)
        {
            isMoovable = true;
            GetComponent<Interactable>().enabled = true;
            GetComponent<VelocityEstimator>().enabled = true;
            GetComponent<Throwable>().enabled = true;
        }

        if(areaNW.GetComponent<areaColScript>().isFilled &&
           areaSW.GetComponent<areaColScript>().isFilled &&
           areaSE.GetComponent<areaColScript>().isFilled &&
           areaNE.GetComponent<areaColScript>().isFilled)
        {
            canBeMooved = true;
        }
    }
}
