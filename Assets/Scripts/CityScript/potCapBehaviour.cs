using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class potCapBehaviour : MonoBehaviour
{
    private bool isMoovable = false;
    public bool canBeMooved = false;

    public GameObject areaNW;
    public GameObject areaSW;
    public GameObject areaSE;
    public GameObject areaNE;


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)126;
    }

    // Update is called once per frame
    void Update()
    {
        if(canBeMooved && isMoovable == false)
        {
            isMoovable = true;
            this.gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)0;
            this.gameObject.AddComponent<Throwable>();
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
