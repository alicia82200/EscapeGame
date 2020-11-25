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
    bool eatText = false;
    bool hungryText = false;
    int frameCounter = 0;

    float initialTime;
    float timeLeft;

    bool timerRunning = true;

    public int chapter;

    // Start is called before the first frame update
    void Start()
    {
        textObjects = gameObject.GetComponentsInChildren<Text>();
        
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        } 
        else if (eatText)
        {
            frameCounter++;
            if (frameCounter > 200)
            {
                textObjects[0].color = new Color(textObjects[0].color.r, textObjects[0].color.g, textObjects[0].color.b, textObjects[0].color.a - 0.003f);
                if (textObjects[0].color.a <= 0)
                {
                    frameCounter = 0;
                    textObjects[0].text = "";
                    textObjects[0].color = new Color(textObjects[0].color.r, textObjects[0].color.g, textObjects[0].color.b, 1.0f);
                    eatText = false;
                }
            }
        }
        else if (hungryText)
        {
            frameCounter++;
            if (frameCounter > 200)
            {
                textObjects[0].color = new Color(textObjects[0].color.r, textObjects[0].color.g, textObjects[0].color.b, textObjects[0].color.a - 0.003f);
                if (textObjects[0].color.a <= 0)
                {
                    frameCounter = 0;
                    textObjects[0].text = "";
                    textObjects[0].color = new Color(textObjects[0].color.r, textObjects[0].color.g, textObjects[0].color.b, 1.0f);
                    hungryText = false;
                }
            }
        }

        if (timerRunning)
        {
            DisplayTime();
        }
    }

    /// <summary>
    /// Affiche le texte de début de partie
    /// </summary>
    private void StartGame()
    {
        switch (chapter)
        {
            case 1:
                {
                    textObjects[0].fontSize = 40;
                    textObjects[0].color = new Color(0.0f, 0.0f, 0.1f, 1.0f);
                    textObjects[0].text = "You wake up in an unknown room ...";
                    textObjects[0].text += "Find your host's cellphone code to erase the compromising photos and run away!";
                    break;
                }
            case 2:
                {
                    textObjects[0].fontSize = 40;
                    textObjects[0].color = new Color(0.0f, 0.0f, 0.1f, 1.0f);
                    textObjects[0].text = "You have come out of the apartment, but the garden is fenced.";
                    textObjects[0].text += "You are hungry, find something to eat.";
                    textObjects[0].text += "Then find a way to open the portal to get home!";
                    break;
                }
            case 3:
                {
                    textObjects[0].fontSize = 40;
                    textObjects[0].color = new Color(0.0f, 0.0f, 0.1f, 1.0f);
                    textObjects[0].text = "You managed to get out of this stranger's house.";
                    textObjects[0].text += "But you don't have any money with you to go home. Seek help.";
                    break;
                }
            default:
                {
                    textObjects[0].text = "Default";
                    break;
                }

        }

        firstText = true;
    }

    /// <summary>
    /// Affiche le texte apres avoir mangé
    /// </summary>
    public void EatWell()
    {
        textObjects[0].fontSize = 40;
        textObjects[0].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        textObjects[0].text = "You have eaten well, you can try to get out now!";
        eatText = true;
    }

    /// <summary>
    /// Affiche le texte apres avoir mangé
    /// </summary>
    public void HungryText()
    {
        textObjects[0].fontSize = 40;
        textObjects[0].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        textObjects[0].text = "You are always hungry, you can't go out like this!";
        hungryText = true;
    }

    /// <summary>
    /// Arrête le chronomètre
    /// </summary>
    public void EndGame()
    {
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
            textObjects[0].text = "You loose !";
            textObjects[0].fontSize = 50;
            textObjects[0].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
