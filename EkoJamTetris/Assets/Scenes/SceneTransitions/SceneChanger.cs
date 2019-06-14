using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    Animator anim;

    int levelToLoad = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            Debug.LogError("No animator in" + transform.name);
        }
    }

    public void FadeToLevel(int level)
    {
        levelToLoad = level;
        anim.SetTrigger("fadeOut");
    }

    public void OnLevelFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
