using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Camera camera;
    public float numberOfMinutes;

    Text[] textObjects;

    bool firstText = false;
    int frameCounter = 0;

    float initialTime;
    float timeLeft;

    bool timerRunning = true;

    int chapter;

    // Start is called before the first frame update
    void Start()
    {
        textObjects = gameObject.GetComponentsInChildren<Text>();
        
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = camera.transform.position + 4.0f * camera.transform.forward;

        if(firstText)
        {
            frameCounter++;
            if(frameCounter > 600)
            {
                textObjects[0].color = new Color(textObjects[0].color.r, textObjects[0].color.g, textObjects[0].color.b, textObjects[0].color.a - 0.003f);
                if(textObjects[0].color.a <= 0)
                {
                    frameCounter = 0;
                    textObjects[0].text = "";
                    textObjects[0].color = new Color(textObjects[0].color.r, textObjects[0].color.g, textObjects[0].color.b, 1.0f);
                    firstText = false;
                    initialTime = Time.realtimeSinceStartup;
                }
            }
        } else if(timerRunning)
        {
            DisplayTime();
        }
    }

    /// <summary>
    /// Affiche le texte de début de partie
    /// </summary>
    private void StartGame()
    {
        textObjects[0].text = "Vous vous réveillez dans une chambre inconnue...";
        textObjects[0].text += "Trouvez le code du portable de votre hôte pour effacer les photos compromettantes et enfuyez vous !";
        firstText = true;
    }

    /// <summary>
    /// Affiche le texte de fin de partie
    /// </summary>
    public void EndGame()
    {
        textObjects[0].fontSize = 40;
        textObjects[0].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        textObjects[0].text = "Vous avez réussi à vous évader, bravo !";
        timerRunning = false;
    }

    /// <summary>
    /// Affiche le chronomètre du temps restant pour l'escape game
    /// </summary>
    private void DisplayTime()
    {
        float timeSinceBeginning = Time.realtimeSinceStartup - initialTime;
        timeLeft = 60 * numberOfMinutes - timeSinceBeginning;
        int minutes = (Mathf.CeilToInt(timeLeft) - Mathf.CeilToInt(timeLeft) % 60) / 60;
        int seconds = Mathf.CeilToInt(timeLeft) % 60;
        if (minutes > 0)
        {
            if (seconds >= 10)
            {
                textObjects[1].text = minutes.ToString() + " : " + seconds.ToString();
            }
            else
            {
                textObjects[1].text = minutes.ToString() + " : 0" + seconds.ToString();
            }
        } else
        {
            textObjects[1].fontSize = 40;
            textObjects[1].color = Color.red;
            if (seconds >= 10)
            {
                textObjects[1].text = seconds.ToString();
            }
            else
            {
                textObjects[1].text = "0" + seconds.ToString();
            }
        }
        if(Mathf.RoundToInt(timeLeft) <= 0)
        {
            textObjects[1].text = "";
            textObjects[0].text = "Vous avez perdu !";
            textObjects[0].fontSize = 50;
            textObjects[0].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
