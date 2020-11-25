using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayChapter1()
    {
        SceneManager.LoadScene(1, 0);
    }

    public void PlayChapter2()
    {
        SceneManager.LoadScene(2, 0);
    }

    public void PlayChapter3()
    {
        SceneManager.LoadScene(3, 0);
    }
}
