﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI1 : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject GameHelpPanel;
    public GameObject MainMenuPanel;
    public GameObject CreditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        //make sure the panels are closed at the start but the buttons are active
        SettingsPanel.SetActive(false);
        GameHelpPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);//this panel has the buttons
    }

    public void PlayGame()
    {
        SceneMaster.OpenScene(1);
    }

    public void OpenSettings()
    {
        SettingsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OpenGameHelp()
    {
        GameHelpPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void CloseAllPanels()
    {
        SettingsPanel.SetActive(false);//close settings
        GameHelpPanel.SetActive(false);//close game help
        CreditsPanel.SetActive(false);//close game help
        MainMenuPanel.SetActive(true);//opens the panel with the buttons
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
