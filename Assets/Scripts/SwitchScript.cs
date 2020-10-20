using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SwitchScript : MonoBehaviour
{
    // Moving switch variables
    public float switchAngle = 180.0f;
    private Vector3 lightRot;
    private Vector3 nightRot;
    private bool changeSwitch = false;

    // Nightmode variables
    public bool nightMode = false;
    private bool changeLight = false;
    public GameObject linkedLight;
    public GameObject nightNotes;

    void Start()
    {
        lightRot = transform.localEulerAngles;
        nightRot = new Vector3(lightRot.x, lightRot.y + switchAngle, lightRot.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSwitch)
        {
            transform.localEulerAngles = nightRot;
            nightMode = true;
        }
        else
        {
            transform.localEulerAngles = lightRot;
            nightMode = false;
        }

        if(changeLight)
        {
            changeLight = false;
            linkedLight.GetComponent<OnOffLight>().lightOff = nightMode;
            nightNotes.SetActive(nightMode);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")    
        {
            changeSwitch = !changeSwitch;
            changeLight = true;
        }
    }

    // Debug with mouse
    void OnMouseDown()
    {
        changeSwitch = !changeSwitch;
        changeLight = true;
    }

}
