using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] Canvas UICanvas; //Assign persistent UI Canvas
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditsMenu;

    public void Start()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void ShowCreditsMenu()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit(); 
    }

}
