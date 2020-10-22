using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEnigmaScript : MonoBehaviour
{
    public bool aIsDone;
    public bool bIsDone;
    public bool cIsDone;
    public bool isLocked = true;


    // Start is called before the first frame update
    void Start()
    {
        aIsDone = false;
        bIsDone = false;
        cIsDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        isUnlocked();
    }

    /// <summary>
    /// Permet de tester si les conditions de resolution de l'enigme sont remplies
    /// </summary>
    public void isUnlocked()
    {
        if (aIsDone && bIsDone && cIsDone)
        {
            isLocked = false;
        }
    }
}
