using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //English Itens
    public string cena;
    public GameObject optionsPanel;
    public GameObject startPanel;
    public GameObject PressAnyKeyPanel;
    public GameObject CreditsPanel;
    public bool PressKeyActive;
    //Portuguese Itens
    public string cena;
    public GameObject optionsPanelPortuguese;
    public GameObject startPanelPortuguese;
    public GameObject PressAnyKeyPanelPortuguese;
    public GameObject CreditsPanelPortuguese;
    public bool PressKeyActivePortuguese;

    // Start is called before the first frame update
    void Start()
    {
        PressKeyActive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PressKeyActive && Input.anyKeyDown)
        {
            GoToMenu();
         }        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(cena);

    }

    public void QuitGame()
    {
        //Unity Editor
        //UnityEditor.EditorApplication.isPlaying = false;
        //Jogo Compilado
        Application.Quit();
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
        startPanel.SetActive(false);
    }
    
    public void BackToMenu()
    {
        optionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        startPanel.SetActive(true);

    }

    public void GoToMenu()
    {
        startPanel.SetActive(true);
        PressAnyKeyPanel.SetActive(false);
        optionsPanel.SetActive(false);
        PressKeyActive = false;
    }

    public void GoToCredits()
    {
        CreditsPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
