using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject title;
    public GameObject menuButtons;
    public GameObject optionButtons;


    int menuPhase = 0;

    void Start()
    {

    }

    void Update()
    {
        if (menuPhase == 0)
        {
            // Fase actual: Titulo
            if (Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0))
            {
                menuPhase = 1;
                TransitionToMenu();
            }
        }
        else if (menuPhase == 1)
        {
            // Fase actual: Menú
            if (Input.GetKey(KeyCode.Escape))
            {
                TransitionToTitle();
            }
        }
        else if (menuPhase == 2)
        {
            // Fase actual: Options
            if (Input.GetKey(KeyCode.Escape))
            {
                TransitionToMenu();
            }
        }
    }
    void TransitionToTitle()
    {
        menuPhase = 0;
        title.SetActive(true);
        menuButtons.SetActive(false);
        optionButtons.SetActive(false);
    }
    void TransitionToMenu()
    {
        menuPhase = 1;
        title.SetActive(false);
        menuButtons.SetActive(true);
        optionButtons.SetActive(false);
    }
    void TransitionToOptions()
    {
        menuPhase = 2;
        title.SetActive(false);
        menuButtons.SetActive(false);
        optionButtons.SetActive(true);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("SelectionScene", LoadSceneMode.Single);
    }
    public void OptionsButton()
    {
        menuPhase = 2;
        TransitionToOptions();
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
 