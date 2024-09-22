using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        MainManager.instance.ResetLevel();
        MainManager.instance.SaveLevel();
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(MainManager.instance.lastLevel);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
}
