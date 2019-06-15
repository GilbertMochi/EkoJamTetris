using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    private static bool gamePaused = false;

    private static GameObject sceneFadingMaster;

    private void Start()
    {
        sceneFadingMaster = GameObject.FindGameObjectWithTag("SceneFadeMaster");
    }

    public static void OpenScene(int level)
    {
        if (sceneFadingMaster != null)
        {
            sceneFadingMaster.GetComponent<SceneChanger>().FadeToLevel(level);
        }
        else
        {
            SceneManager.LoadScene(level);
        }
    }

    public static void OpenMainMenu()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public static void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;//stops gametime
    }

    public static void ContinueGame()
    {
        gamePaused = false;
        Time.timeScale = 1f; //resumes gametime
    }

    public static bool getIsGamePaused()
    {
        return gamePaused;
    }
}
