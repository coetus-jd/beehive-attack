using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    //items
    public string cena;    
    //English Itens    
    public GameObject optionsPanel;
    public GameObject startPanel;
    public GameObject PressAnyKeyPanel;
    public GameObject CreditsPanel;
    public GameObject LanguagePanel;
    public bool PressKeyActive;
    

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
    public void GoToLanguage()
    {
        optionsPanel.SetActive(false);
        LanguagePanel.SetActive(true);
    } 
    public void BackToOptions()
    {
        optionsPanel.SetActive(true);
        LanguagePanel.SetActive(false);
    }
}
