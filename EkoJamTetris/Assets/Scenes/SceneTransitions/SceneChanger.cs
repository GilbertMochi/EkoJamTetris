using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator anim;

    int levelToLoad = 0;
   
    public void FadeToLevel(int level)
    {
        levelToLoad = level;
        anim.SetTrigger("fadeOut");
    }

    public void onLevelFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
