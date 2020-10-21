using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public class OpenBox : MonoBehaviour
{
    public float smooth = 2.0f;
    public float BoxOpenAngle = -60.0f;

    private Vector3 defaultRot;
    private Vector3 openRot;
    private bool unlock = false;
    private bool open = false;

    // Use this for initialization
    void Start()
    {

        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y, defaultRot.z - BoxOpenAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (unlock)
        {
            defaultRot = Vector3.Lerp(defaultRot, openRot, Time.deltaTime);
            transform.eulerAngles = defaultRot;
        }
        

        if (GameObject.FindWithTag("Key").GetComponent<keyBehaviour>().isUsed && open)   //peut s'ouvrir si on a la clé en main
        {
            unlock = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")    //ouvrir le coffre en le touchant 
        {
            open = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            open = false;
        }
    }

    private void OnMouseDown()
    {
        open = true;
    }
}
