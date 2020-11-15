using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class BoxBehaviour : MonoBehaviour
{

    /* VARIABLES */
    //Les prefabs de chaque bouton
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public GameObject g5;
    public GameObject g6;
    public GameObject g7;
    public GameObject g8;
    public GameObject g9;
    public GameObject g10;
    public GameObject g11;
    public GameObject g12;

    public bool isHeld;  //si la boite est tenue dans la main du joueur ou pas
    public float angle;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 menuScroll;
    public SteamVR_Action_Boolean selectNumber;
    public Vector2 menuPosition;
    public bool isLocked = true;
    public Text tryCode; //Le code entré par l'utilisateur
    public Text screenText;
    public GameObject canvas;

    private buttonBehavior selectedButton;
    private buttonBehavior formerButton;
    private int idCurrentButton;
    private int idFormerButton;
    private float espacementBoutons;
    private float rayonBoutons;
    private bool unChiffreEnPlusPasPlus = true; // Pour ajouter un chiffre au code un par un
    public bool initWheelOK;
    public string code; // Le code à trouver pour déverouiller
    private List<GameObject> gObjList;
    private List<buttonBehavior> boutons;
    private Transform originalParent;


    // START
    void Start()
    {
        //Paramètres de la roue de chiffres
        espacementBoutons = 30.0f;
        rayonBoutons = 0.25f;

        //Ajout des gameobjects à une liste (pour un futur parcourt de cette liste)
        gObjList = new List<GameObject>();
        gObjList.Add(g1);
        gObjList.Add(g2);
        gObjList.Add(g3);
        gObjList.Add(g4);
        gObjList.Add(g5);
        gObjList.Add(g6);
        gObjList.Add(g7);
        gObjList.Add(g8);
        gObjList.Add(g9);
        gObjList.Add(g10);
        gObjList.Add(g11);
        gObjList.Add(g12);

        //Initialisation de quelques variables
        isHeld = false;
        tryCode.text = "";
        boutons = new List<buttonBehavior>();
        initWheelOK = true;
        originalParent = transform.parent;
        screenText.text = "Code :";

        //Par défault, c'est le bouton 'en haut' de la roue qui est sélectionné (bouton correction => g10)
        selectedButton = g10.GetComponent<buttonBehavior>();
        selectedButton.Select();
        idCurrentButton = 10;
        idFormerButton = idCurrentButton;

        canvas = transform.GetChild(0).gameObject;
        canvas.SetActive(false);
    }

    // UPDATE
    void Update()
    {
        isHeld = GetComponent<Throwable>().GetAttached();

        // Si on prend la boite, alors la roue de choix du code apparaît
        if (isHeld)
        {
            if (initWheelOK)
            {
                initiateButtonWheel();
                initWheelOK = false;
            }

            //Activation du canvas 
            canvas.SetActive(true);

            // On récupère la position du pouce sur le trackpad de la main droite
            menuPosition = menuScroll.GetAxis(SteamVR_Input_Sources.RightHand);

            // On calcule l'angle
            angle = Mathf.Atan(menuPosition[0] / menuPosition[1]);

            //On change le bouton en fonction de l'angle
            SwitchButton(angle);

            // En mode debug souris
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchButtonDebug();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                addNumber(idCurrentButton);
            }
#endif

            //On ajoute un nombre avec le bouton de la main gauche
            if (selectNumber.GetState(SteamVR_Input_Sources.LeftHand))
            {
                addNumber(idCurrentButton);
            }

            //Quand on relâche la gâchette on peut rajouter un chiffre
            if (!selectNumber.GetState(SteamVR_Input_Sources.LeftHand))
            {
                unChiffreEnPlusPasPlus = true;
            }
        }

        else
        {
            destroyButtonWheel();
            initWheelOK = true;
            canvas.SetActive(false);
        }

        if (isLocked == false)
        {
            destroyButtonWheel();
            initWheelOK = true;
            canvas.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }


    }

    /* -------------------------------------- METHODS ------------------------------------ */ 

#if DEBUG
    /// <summary>
    /// Change la sélection du bouton [debug]
    /// </summary>
    public void SwitchButtonDebug()
    {
        if (idFormerButton == 12)
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
        // Il y a 12 boutons => répartition en 12 zones
        // De manière empirique : on a regardé l'angle calculé sur l'inspecteur en runtime pour savoir quel id correspond à quel angle
        if (menuPosition[0] >= 0)
        {
            if (0.0f < angle && angle <= Mathf.PI / 12)
            {
                idCurrentButton = 4;
            }

            if (Mathf.PI / 12 < angle && angle <= 3 * Mathf.PI / 12)
            {
                idCurrentButton = 3;
            }

            if (3 * Mathf.PI / 12 < angle && angle <= 5 * Mathf.PI / 12)
            {
                idCurrentButton = 2;
            }

            if ((5 * Mathf.PI / 12 < angle && angle <= Mathf.PI / 2) || (-Mathf.PI / 2 <= angle && angle <= -5 * Mathf.PI / 12))
            {
                idCurrentButton = 1;
            }

            if (-5 * Mathf.PI / 12 < angle && angle <= -3 * Mathf.PI / 12)
            {
                idCurrentButton = 12;
            }

            if (-3 * Mathf.PI / 12 < angle && angle <= -Mathf.PI / 12)
            {
                idCurrentButton = 11;
            }

            if (-Mathf.PI / 12 < angle && angle <= 0.0)
            {
                idCurrentButton = 10;
            }
        }

        if (menuPosition[0] < 0)
        {
            if (0.0f < angle && angle <= Mathf.PI / 12)
            {
                idCurrentButton = 10;
            }

            if (Mathf.PI / 12 < angle && angle <= 3 * Mathf.PI / 12)
            {
                idCurrentButton = 9;
            }

            if (3 * Mathf.PI / 12 < angle && angle <= 5 * Mathf.PI / 12)
            {
                idCurrentButton = 8;
            }

            if ((5 * Mathf.PI / 12 < angle && angle <= Mathf.PI / 2) || (-Mathf.PI / 2 <= angle && angle <= -5 * Mathf.PI / 12))
            {
                idCurrentButton = 7;
            }

            if (-5 * Mathf.PI / 12 < angle && angle <= -3 * Mathf.PI / 12)
            {
                idCurrentButton = 6;
            }

            if (-3 * Mathf.PI / 12 < angle && angle <= -Mathf.PI / 12)
            {
                idCurrentButton = 5;
            }

            if (-Mathf.PI / 12 < angle && angle <= 0.0)
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
    /// Permet d'ajouter le chiffre validé (via son id) au code d'essai
    /// </summary>
    /// <param name="id"></param>
    public void addNumber(int id)
    {

        //Si on appuie sur la gâchette de la main gauche, alors on valide l'entrée d'un chiffre du code
        // Les id 10, 11 et 12 correspondent aux boutons Corriger, Valider et Annuler (respectivement)
        if (tryCode.text.Length < 2 && unChiffreEnPlusPasPlus)
        {
            switch (idCurrentButton)
            {
                case 11:
                    isLocked = tryUnlock(tryCode.text);
                    unChiffreEnPlusPasPlus = false;
                    break;

                case 12:
                    tryCode.text = "";
                    unChiffreEnPlusPasPlus = false;
                    break;

                case 10:
                    tryCode.text = tryCode.text.Substring(0, tryCode.text.Length - 1);
                    unChiffreEnPlusPasPlus = false;
                    break;

                default:
                    tryCode.text += id;
                    unChiffreEnPlusPasPlus = false;
                    break;
            }
        }

        if (tryCode.text.Length == 2)
        {
            switch (idCurrentButton)
            {
                case 11:
                    isLocked = tryUnlock(tryCode.text);
                    unChiffreEnPlusPasPlus = false;
                    break;

                case 12:
                    tryCode.text = "";
                    unChiffreEnPlusPasPlus = false;
                    break;

                case 10:
                    tryCode.text = tryCode.text.Substring(0, tryCode.text.Length - 1);
                    unChiffreEnPlusPasPlus = false;
                    break;
            }

            if (tryUnlock(tryCode.text))
            {
                isLocked = false;
            }

            else
            {
                tryCode.text = "";
                screenText.color = Color.red;
                screenText.text = "Wrong \n";
            }

        }
    }

    /// <summary>
    /// Permet de tester si le code d'essai est juste ou non 
    /// </summary>
    /// <param name="trycode"></param>
    public bool tryUnlock(string trycode)
    {
        if (trycode == code)
        {
            return (true);
        }

        else
        {
            return (false);
        }
    }

    /// <summary>
    /// Initialise les boutons de la boite
    /// </summary>
    /// <param name="data"></param>
    public void initiateButtonWheel()
    {
        foreach (GameObject i in gObjList)
        {
            GameObject bouton = Instantiate(i, transform);          // Vector3(0.6f, 0.78f, 0.0f) est la position du centre souhaitée
            bouton.transform.localPosition = new Vector3(0.6f + rayonBoutons * Mathf.Cos(Mathf.Deg2Rad * gObjList.IndexOf(i) * espacementBoutons), 0.78f, rayonBoutons * Mathf.Sin(Mathf.Deg2Rad * gObjList.IndexOf(i) * espacementBoutons));
            bouton.transform.localScale = bouton.transform.localScale *3 / 4;
            bouton.transform.eulerAngles = new Vector3(bouton.transform.eulerAngles.x, bouton.transform.eulerAngles.y + 180, bouton.transform.eulerAngles.z);
            boutons.Add(bouton.GetComponent<buttonBehavior>());
        }
    }

    /// <summary>
    /// Détruit les boutons de la boite
    /// </summary>
    /// <param name="data"></param>
    public void destroyButtonWheel()
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
