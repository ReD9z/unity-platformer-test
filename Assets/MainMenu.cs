using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
