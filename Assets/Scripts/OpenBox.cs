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

    public bool isTouched = false;

    // Use this for initialization
    void Start()
    {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y, defaultRot.z - BoxOpenAngle);
    }

    // Update is called once per frame
    void Update()
    {
        isTouched = GetComponent<Interactable>().isHovering;

        if(isTouched)
        {
            open = true;
        }

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

    private void OnMouseDown()
    {
        open = true;
    }
}
