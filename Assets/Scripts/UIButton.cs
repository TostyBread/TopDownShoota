using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public MainMenuController mainMenuController;
    public GameObject mainMenuUI; // Main Menu UI toggle
    public GameObject functionMenuUI; // Function Menu UI toggle
    public GameObject levelMenuUI; // Level select Menu UI toggle
    private void Start()
    {
        OnMainMenuButtonClicked();
    }
    public void OnPlayButtonClicked() // when play button clicked, run this
    {
        mainMenuController.MoveToLevelSelectMenu();
        mainMenuUI.SetActive(false);
        functionMenuUI.SetActive(false);
        levelMenuUI.SetActive(true);
    }
    public void OnFunctionButtonClicked() // when fucntion button clicked, run this
    {
        mainMenuController.MoveToFunctionMenu();
        mainMenuUI.SetActive(false);
        functionMenuUI.SetActive(true);
        levelMenuUI.SetActive(false);
    }
    public void OnMainMenuButtonClicked() // when main menu button clicked, run this
    {
        mainMenuController.MoveToMainMenu();
        mainMenuUI.SetActive(true);
        functionMenuUI.SetActive(false);
        levelMenuUI.SetActive(false);
    }
}
