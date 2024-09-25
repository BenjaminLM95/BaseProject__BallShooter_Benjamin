using System.Collections;
using UnityEngine.UI;
using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2022

public class UIManager : MonoBehaviour
{
    //Script references
    public GameObject ball;
    private BallManager _ballController;

    //private GameObject gameManager;
    //private GameManager _gameManager;

    public GameObject gamePlayUI;
    public GameObject mainMenuUI;
    public GameObject levelCompleteUI;
    public GameObject gameCompleteUI;
    public GameObject levelFailedUI;
    public GameObject pauseMenuUI;





    public Text modeText;    
    public Text ShotsLeftCount;

    public Text LevelCount;
    
    public void UpdateShotsleft(int count)
    {        
        ShotsLeftCount.text = count.ToString();
    }

    public void UpdateLevelCount(int count)
    {
        LevelCount.text = count.ToString();
    }

    public void UIMainMenu()
    {
        DisableAllUIPanels();
        mainMenuUI.SetActive(true);
    }

    public void UIGamePlay() // same as UIRolling.. consider merging them into one method for UIGameplay? make sure there are no issues first
    {
        DisableAllUIPanels();
        gamePlayUI.SetActive(true);
    }



    public void UILevelFailed()
    {
        DisableAllUIPanels();
        levelFailedUI.SetActive(true);
    }

    public void UILevelComplete()
    {
        DisableAllUIPanels();
        levelCompleteUI.SetActive(true);
    }

    public void UIGameComplete()
    {
        DisableAllUIPanels();
        gameCompleteUI.SetActive(true);
    }

    public void UIPaused()
    {
        DisableAllUIPanels();
        pauseMenuUI.SetActive(true);
    }

    public void DisableAllUIPanels()
    {
        gamePlayUI.SetActive(false);
        mainMenuUI.SetActive(false);
        levelCompleteUI.SetActive(false);
        gameCompleteUI.SetActive(false);
        levelFailedUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }



}
