﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour
{

public GameObject PausePanel;
public GameObject gameoverpanel;

private void Start() {
    PausePanel.SetActive(false);
    gameoverpanel.SetActive(false);
}
    // Update is called once per frame
    public void Pause()
    {
        SceneMaster.PauseGame();
        PausePanel.SetActive(true);
    }
    public void Continue()
    {
        SceneMaster.ContinueGame();
        PausePanel.SetActive(false);
    }

    public void Quit()
    {
        SceneMaster.OpenMainMenu();
        PausePanel.SetActive(false);
        gameoverpanel.SetActive(false);
    }
}
