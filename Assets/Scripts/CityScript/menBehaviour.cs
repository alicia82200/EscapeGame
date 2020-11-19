using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class menBehaviour : MonoBehaviour
{
    //Les prefabs de chaque bouton
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public GameObject c4;

    public bool isTouch;  //si l'homme est touché par le joueur
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
    private int frameCounter = 251;

    //Enigma variables
    private bool isHelped = false;
    public bool isHelping = false;
    public int garbageInTrash = 0;
    public GameObject ring;

    // Start is called before the first frame update
    void Start()
    {
        //Paramètres de la roue de chiffres
        espacementBoutons = 90.0f;
        rayonBoutons = 0.2f;

        //Ajout des gameobjects à une liste (pour un futur parcourt de cette liste)
        gObjList = new List<GameObject>();
        gObjList.Add(c1);
        gObjList.Add(c2);
        gObjList.Add(c3);
        gObjList.Add(c4);

        //Initialisation de quelques variables
        isTouch = false;
        boutons = new List<buttonBehavior>();
        initChoiceOK = true;
        originalParent = transform.parent;
        screenText.text = "Have you seen all this garbage? People are dirty! And I have to collect everything, it's unfair.";

        //Par défault, c'est le premier bouton qui est sélectionné
        selectedButton = c1.GetComponent<buttonBehavior>();
        selectedButton.Select();
        idCurrentButton = 1;
        idFormerButton = idCurrentButton;

        canvas = transform.GetChild(0).gameObject;
        canvas.SetActive(false);
        ring.SetActive(false);

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouch = GetComponent<Interactable>().isHovering;

        if(garbageInTrash == 6)
        {
            isHelped = true;
        }

        frameCounter++;

        if (frameCounter > 250)
        {
            anim.SetBool("Wait", false);
            frameCounter = 0;
        }
        else
        {
            anim.SetBool("Wait", true);
        }

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
            screenText.text = "Have you seen all this garbage? People are dirty! And I have to collect everything, it's unfair.";
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
                idCurrentButton = 2;
            }

            if (-Mathf.PI / 2 < angle && angle <= 0.0f)
            {
                idCurrentButton = 3;
            }
        }

        if (menuPosition[0] < 0)
        {
            if (0.0f < angle && angle <= Mathf.PI / 2)
            {
                idCurrentButton = 4;
            }

            if (-Mathf.PI / 2 < angle && angle <= 0.0f)
            {
                idCurrentButton = 1;
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
                        screenText.text = "No, that will be fine thanks. You have already done a lot.";
                    }
                    else
                    {
                        screenText.text = "Yes ! Collect trash and put it in the trash cans next to the benches.";
                        isHelping = true;
                    }
                }
                break;
            case 2:
                {
                    screenText.text = "Yes it's my job so what? People are animals! I am not a zoo cleaner.";
                }
                break;
            case 3:
                {
                    if (isHelped)
                    {
                        screenText.text = "Yes, I did find one. I put it on the bench for you, get it.";
                        ring.SetActive(true);
                    }
                    else
                    {
                        screenText.text = "Maybe or maybe not ... A ring can be expensive you know?";
                    }
                }
                break;
            case 4:
                {
                    if (isHelped)
                    {
                        screenText.text = "Sorry about earlier, I took you for a beggar. However, I have no money on me.";
                    }
                    else
                    {
                        screenText.text = "Haha, very funny! You really think I'm going to give you money, you homeless man!";
                    }
                }
                break;
            default:
                {
                    screenText.text = "Have you seen all this garbage? People are dirty! And I have to collect everything, it's unfair.";
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
            bouton.transform.localPosition = new Vector3(1f + rayonBoutons * Mathf.Cos( Mathf.Deg2Rad * (gObjList.IndexOf(i) * espacementBoutons + 45f)), 1.5f + rayonBoutons * Mathf.Sin(Mathf.Deg2Rad * (gObjList.IndexOf(i) * espacementBoutons + 45f)), 0f);
            bouton.transform.eulerAngles = new Vector3(bouton.transform.eulerAngles.x - 90f, bouton.transform.eulerAngles.y, bouton.transform.eulerAngles.z + 180f);
            bouton.transform.localScale = bouton.transform.localScale /8;
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
}
