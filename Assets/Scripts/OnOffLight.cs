using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLight : MonoBehaviour
{
    public bool lightOff = false;
    Light thisLight;
    Renderer thisRenderer;
    public Material onMaterial;
    public Material offMaterial;

    // Start is called before the first frame update
    void Start()
    {
        thisLight = gameObject.GetComponent<Light>();
        thisRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightOff)
        {
            thisLight.enabled = false;
            thisRenderer.material = offMaterial;
        }
        else
        {
            thisLight.enabled = true;
            thisRenderer.material = onMaterial;
        }
    }
}
