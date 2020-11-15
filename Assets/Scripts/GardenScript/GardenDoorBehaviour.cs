using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class GardenDoorBehaviour : MonoBehaviour
{
    private bool isTouch;
    public bool foodEaten;
    private bool isUnlock = false;
    public GameObject gardenLockObject;

    // Start is called before the first frame update
    void Start()
    {
        isTouch = false;
        foodEaten = false;

    }

    // Update is called once per frame
    void Update()
    {
        isUnlock = !gardenLockObject.GetComponent<gardenLock>().isLocked;
        isTouch = GetComponent<Interactable>().isHovering;

        if (isTouch)
        {
            if (foodEaten)
            {
                if (isUnlock)
                {
                    GameObject.FindGameObjectWithTag("MainCanva").GetComponent<TextDisplay>().EndGame();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                GameObject.FindGameObjectWithTag("MainCanva").GetComponent<TextDisplay>().HungryText();
            }
        }
    }
}
