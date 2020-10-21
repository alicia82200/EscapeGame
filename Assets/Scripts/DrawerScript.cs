using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DrawerScript : MonoBehaviour
{
    public float DrawerOpenPosition = 0.3f;
    private Vector3 defaultPos;
    private Vector3 openPos;
    private bool open = false;
    public GameObject parent;

    void Start()
    {
        defaultPos = parent.transform.position;
        openPos = new Vector3(defaultPos.x, defaultPos.y, defaultPos.z + DrawerOpenPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            parent.transform.position = openPos;
        }
        else
        {
            parent.transform.position = defaultPos;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")    //ouvrir le coffre en le touchant 
        {
            open = !open;
        }
    }

    void OnMouseDown()
    {
        open = !open;
    }
}
