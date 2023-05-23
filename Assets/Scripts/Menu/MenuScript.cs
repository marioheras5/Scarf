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

    bool delaying = false;
    int menuPhase = 0;

    void Update()
    {
        if (delaying) return;
        if (menuPhase == 0)
        {
            // Fase actual: Titulo
            if (Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0))
            {
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
        StartCoroutine(Delay());
    }
    public void TransitionToMenu()
    {
        menuPhase = 1;
        title.SetActive(false);
        menuButtons.SetActive(true);
        optionButtons.SetActive(false);
        StartCoroutine(Delay());
    }
    void TransitionToCoop()
    {
        menuPhase = 2;
        title.SetActive(false);
        menuButtons.SetActive(false);
        optionButtons.SetActive(true);
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        delaying = true;
        yield return new WaitForSeconds(0.25f);
        delaying = false;
    }
    public void StartButton()
    {
        SceneManager.LoadScene("SelectionScene", LoadSceneMode.Single);
    }
    public void OptionsButton()
    {
        TransitionToCoop();
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
 