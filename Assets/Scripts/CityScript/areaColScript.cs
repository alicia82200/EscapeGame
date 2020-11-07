using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaColScript : MonoBehaviour
{
    public bool isFilled = false;
    public int number;
    private int addition = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(addition == number)
        {
            isFilled = true;
        }
        else
        {
            isFilled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coupon")
        {
            addition += other.gameObject.GetComponent<couponBehaviour>().num;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Coupon")
        {
            addition -= other.gameObject.GetComponent<couponBehaviour>().num;
        }
    }
}
