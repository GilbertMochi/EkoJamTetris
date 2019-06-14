using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    private static bool gamePaused = false;

    public static void OpenScene(int level)
    {
        SceneManager.LoadScene(level);
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
