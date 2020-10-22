using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public class OpenDoor : MonoBehaviour {

	public float smooth = 2.0f;
	public float DoorOpenAngle = 90.0f;
    private float rayonDoor = 1f;

	public AudioClip OpenAudio;
	public AudioClip CloseAudio;
	private bool AudioS;

	private Vector3 defaultRot;
	private Vector3 openRot;
    private Vector3 defaultPos;
    private Vector3 openPos;
    public bool open = false;
	private bool enter = false;

    public bool finalDoor;

	// Use this for initialization
	void Start () {

			defaultRot = transform.eulerAngles;
			openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
            defaultPos = transform.position;
            openPos = new Vector3(defaultPos.x - rayonDoor*(1 - Mathf.Cos((2*Mathf.PI* DoorOpenAngle)/360)), defaultPos.y, defaultPos.z + rayonDoor * Mathf.Sin((2 * Mathf.PI * DoorOpenAngle) / 360));
        }
	
	// Update is called once per frame
	void Update () {
		if (open) {
            transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);
            transform.position = Vector3.Slerp(transform.position, openPos, Time.deltaTime * smooth);
            if (AudioS == false)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(OpenAudio);
                AudioS = true;
            }

            if(finalDoor)
            {
                TextDisplay message = new TextDisplay();
                message.EndGame();
            }
            

        } else {
            transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
            transform.position = Vector3.Slerp(transform.position, defaultPos, Time.deltaTime * smooth);
            if (AudioS == true) {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (CloseAudio);
				AudioS = false;
			}
			

		}
        
        if (finalDoor == false)
        {
            if (GameObject.FindWithTag("Phone").GetComponent<phoneBehavior>().isLocked == false && enter)   //peut s'ouvrir si on a résolu l'enigme du telephone et qu'on selectionne la porte
            {
                open = !open;
            }
        }
        else if (finalDoor)
        {
            if (GameObject.FindWithTag("LastEnigma").GetComponent<LastEnigmaScript>().isLocked == false && enter)   //peut s'ouvrir si on a résolu l'enigme du telephone et qu'on selectionne la porte
            {
                open = !open;
            }
        }
        
    }

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			enter = true;
			}
		}

    void OnTriggerExit(Collider col)
    {
	    if (col.tag == "Player") {
		    enter = false;
	    }
    }

    private void OnMouseDown()
    {
        enter = true;
    }

    private void OnMouseUp()
    {
        enter = false;
    }
}