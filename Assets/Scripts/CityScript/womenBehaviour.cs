using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class womenBehaviour : MonoBehaviour
{
    //Les prefabs de chaque bouton
    public GameObject c1;
    public GameObject c20;
    public GameObject c30;
    public GameObject c4;

    public bool isTouch;  //si la fille est touché par le joueur
    public float angle;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 menuScroll;
    public SteamVR_Action_Boolean selectNumber;
    public Vector2 menuPosition;
    public Text screenText;
    public GameObject canvas;

    private buttonBehavior selectedButton;
    private buttonBehavior formerButton;
    private int idCurrentButton;
    private int idFormerButton;
    private float espacementBoutons;
    private float rayonBoutons;
    public bool initChoiceOK;
    private List<GameObject> gObjList;
    private List<buttonBehavior> boutons;
    private Transform originalParent;

    //For animation
    Animator anim;

    //Enigma variables
    public bool isHelped = false;

    // Start is called before the first frame update
    void Start()
    {
        //Paramètres de la roue de chiffres
        espacementBoutons = 90.0f;
        rayonBoutons = 0.2f;

        //Ajout des gameobjects à une liste (pour un futur parcourt de cette liste)
        gObjList = new List<GameObject>();
        gObjList.Add(c1);
        gObjList.Add(c20);
        gObjList.Add(c30);
        gObjList.Add(c4);

        //Initialisation de quelques variables
        isTouch = false;
        boutons = new List<buttonBehavior>();
        initChoiceOK = true;
        originalParent = transform.parent;
        screenText.text = "This is terrible! I lost my ring. I really care about it, sniff!";

        //Par défault, c'est le premier bouton qui est sélectionné
        selectedButton = c1.GetComponent<buttonBehavior>();
        selectedButton.Select();
        idCurrentButton = 1;
        idFormerButton = idCurrentButton;

        canvas = transform.GetChild(0).gameObject;
        canvas.SetActive(false);

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouch = GetComponent<Interactable>().isHovering;

        // Si on touche l'homme, alors les choix apparaissent et l'animation de discution se joue
        if (isTouch)
        {
            anim.SetBool("Talking", true);

            if (initChoiceOK)
            {
                initiateButton();
                initChoiceOK = false;
            }

            //Activation du canvas 
            canvas.SetActive(true);

            // On récupère la position du pouce sur le trackpad de la main droite
            menuPosition = menuScroll.GetAxis(SteamVR_Input_Sources.RightHand);

            // On calcule l'angle
            angle = Mathf.Atan(menuPosition[0] / menuPosition[1]);

            //On change le bouton en fonction de l'angle
            SwitchButton(angle);

            // En mode debug
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchButtonDebug();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                chooseChoice(idCurrentButton);
            }
#endif

            //On choisit un dialogue avec le bouton de la main gauche
            if (selectNumber.GetState(SteamVR_Input_Sources.LeftHand))
            {
                chooseChoice(idCurrentButton);
            }

            //Quand on relâche la gâchette on peut 
            if (!selectNumber.GetState(SteamVR_Input_Sources.LeftHand))
            {
            }
        }
        else
        {
            anim.SetBool("Talking", false);
            destroyButton();
            initChoiceOK = true;
            if (isHelped)
            {
                screenText.text = "Hello";
            }
            else
            {
                screenText.text = "This is terrible! I lost my ring. I really care about it, sniff!";
            }
            canvas.SetActive(false);
        }
    }

#if DEBUG
    /// <summary>
    /// Change la sélection du bouton [debug]
    /// </summary>
    public void SwitchButtonDebug()
    {
        if (idFormerButton == 4)
        {
            idCurrentButton = 1;
        }
        else
        {
            idCurrentButton = idFormerButton + 1;
        }
        // Enfin, selon l'id du bouton, on fait l'échange de sélection entre les boutons
        if (idFormerButton != idCurrentButton)
        {
            selectedButton = boutons[idCurrentButton - 1];
            formerButton = boutons[idFormerButton - 1];

            selectedButton.Select();
            formerButton.Deselect();

            idFormerButton = idCurrentButton;
        }
    }
#endif
    /// <summary>
    /// Change le bouton sélectionné en fonction de l'angle 
    /// </summary>
    /// <param name="angle"></param>
    public void SwitchButton(float angle)
    {
        // Il y a 4 boutons => répartition en 4 zones
        // De manière empirique : on a regardé l'angle calculé sur l'inspecteur en runtime pour savoir quel id correspond à quel angle
        if (menuPosition[0] >= 0)
        {
            if (0.0f < angle && angle <= Mathf.PI / 2)
            {
                idCurrentButton = 1;
            }

            if (Mathf.PI / 2 < angle && angle <= Mathf.PI)
            {
                idCurrentButton = 2;
            }

            if (Mathf.PI < angle && angle <= 3 * Mathf.PI / 2)
            {
                idCurrentButton = 3;
            }
            if (3 * Mathf.PI / 2 < angle && angle <= 0.0)
            {
                idCurrentButton = 4;
            }
        }

        if (menuPosition[0] < 0)
        {
            if (0.0f < angle && angle <= Mathf.PI / 2)
            {
                idCurrentButton = 1;
            }

            if (Mathf.PI / 2 < angle && angle <= Mathf.PI)
            {
                idCurrentButton = 2;
            }

            if (Mathf.PI < angle && angle <= 3 * Mathf.PI / 2)
            {
                idCurrentButton = 3;
            }
            if (3 * Mathf.PI / 2 < angle && angle <= 0.0)
            {
                idCurrentButton = 4;
            }
        }

        // Enfin, selon l'id du bouton, on fait l'échange de sélection entre les boutons
        if (idFormerButton != idCurrentButton)
        {
            selectedButton = boutons[idCurrentButton - 1];
            formerButton = boutons[idFormerButton - 1];

            selectedButton.Select();
            formerButton.Deselect();

            idFormerButton = idCurrentButton;
        }
    }

    /// <summary>
    /// Permet de selectionner un des choix possibles
    /// </summary>
    /// <param name="id"></param>
    public void chooseChoice(int id)
    {
        //Si on appuie sur la gâchette de la main gauche, alors on valide l'entrée d'un choix
        switch (id)
        {
            case 1:
                {
                    if (isHelped)
                    {
                        screenText.text = "I'm relieved, thank you again for what you did! It will be okay now.";
                    }
                    else
                    {
                        screenText.text = "Can you help me find my ring? Its very important !";
                    }
                }
                break;
            case 2:
                {
                    if (isHelped)
                    {
                        screenText.text = "I had lost it while walking in the park but I found it!";
                    }
                    else
                    {
                        screenText.text = "I was walking in the park and when I sat down I noticed that I didn't have it anymore.";
                    }
                }
                break;
            case 3:
                {
                    if (isHelped)
                    {
                        screenText.text = "Yes, everything is fine now!";
                    }
                    else
                    {
                        screenText.text = "No ! I'm in a panic! I lost my grandmother's ring!";
                    }
                }
                break;
            case 4:
                {
                    if (isHelped)
                    {
                        screenText.text = "Sorry about earlier. Here, it's an unused metro ticket, thank you for everything!";
                        GameObject.FindGameObjectWithTag("MainCanva").GetComponent<TextDisplay>().EndGame();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        screenText.text = "Look, I'm not in the mood. Do it on your own!";
                    }
                }
                break;
            default:
                {
                    if (isHelped)
                    {
                        screenText.text = "Hello";
                    }
                    else
                    {
                        screenText.text = "This is terrible! I lost my ring. I really care about it, sniff!";
                    }
                }
                break;

        }
    }

    /// <summary>
    /// Initialise les boutons du téléphone
    /// </summary>
    /// <param name="data"></param>
    public void initiateButton()
    {
        foreach (GameObject i in gObjList)
        {
            GameObject bouton = Instantiate(i, transform);
            bouton.transform.localPosition = new Vector3(-0.5f + rayonBoutons * Mathf.Cos(Mathf.Deg2Rad * (gObjList.IndexOf(i) * espacementBoutons + 45f)), 1f + rayonBoutons * Mathf.Sin(Mathf.Deg2Rad * (gObjList.IndexOf(i) * espacementBoutons + 45f)), 0f);
            bouton.transform.eulerAngles = new Vector3(bouton.transform.eulerAngles.x - 90f, bouton.transform.eulerAngles.y, bouton.transform.eulerAngles.z + 180f);
            bouton.transform.localScale = bouton.transform.localScale / 8;
            boutons.Add(bouton.GetComponent<buttonBehavior>());
        }
    }

    /// <summary>
    /// Détruit les boutons du téléphone
    /// </summary>
    /// <param name="data"></param>
    public void destroyButton()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "button")
            {
                GameObject.Destroy(child.gameObject);
                boutons.Remove(child.GetComponent<buttonBehavior>());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ring")
        {
            GameObject.Destroy(other);
            isHelped = true;
        }
    }
}
