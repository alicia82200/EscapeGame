using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class couponBehaviour : MonoBehaviour
{

    public int num;
    public List<Material> materials;

    void Start()
    {
        switch (num)
        {
            case 1:
                {
                    GetComponent<MeshRenderer>().material = materials[0];
                }
                break;
            case 5:
                {
                    GetComponent<MeshRenderer>().material = materials[1];
                }
                break;
            case 10:
                {
                    GetComponent<MeshRenderer>().material = materials[2];
                }
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
