using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(1);

    }
    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene(0);

    }
    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
