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

    public bool isTouched = false;
    public bool wasTouched = false;
    private int frameCounter = 0;

    void Start()
    {
        defaultPos = parent.transform.position;
        openPos = new Vector3(defaultPos.x, defaultPos.y, defaultPos.z + DrawerOpenPosition);
    }

    // Update is called once per frame
    void Update()
    {
        isTouched = GetComponent<Interactable>().isHovering;

        if (isTouched && wasTouched == false)
        {
            open = !open;
            wasTouched = true;
        }
        else if (wasTouched)
        {
            frameCounter++;
            if (frameCounter == 200)
            {
                wasTouched = false;
                frameCounter = 0;
            }
        }

        if (open)
        {
            parent.transform.position = openPos;
        }
        else
        {
            parent.transform.position = defaultPos;
        }

    }

    void OnMouseDown()
    {
        open = !open;
    }
}
