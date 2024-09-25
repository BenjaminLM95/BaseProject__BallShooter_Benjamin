using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class GameState_GameInit : IGameState
{
    // This state is used the first time the game is Initialized (launched/opened)
    // It will be used to set up all default settings
    // I realize this likley could be done in the MainMenu state as returning to it could count as a game reset of sorts... but it seems cleaner to have it's own state for this

    public void EnterState(GameStateManager gameStateManager)
    {
        Cursor.visible = false;

        gameStateManager._cameraManager.UseMainMenuCamera();

        // Enable Ball & AimGuide
        gameStateManager._ballManager.ball.SetActive(true);         
        gameStateManager._ballManager.aimGuide.SetActive(true);     

        // Enable all UI Panels
        gameStateManager._uIManager.gamePlayUI.SetActive(true);
        gameStateManager._uIManager.mainMenuUI.SetActive(true);
        gameStateManager._uIManager.levelCompleteUI.SetActive(true);
        gameStateManager._uIManager.levelFailedUI.SetActive(true);
        gameStateManager._uIManager.gameCompleteUI.SetActive(true);
        gameStateManager._uIManager.pauseMenuUI.SetActive(true);

        // Switch to MainMenu state
        gameStateManager.SwitchToState(new GameState_MainMenu());

        // Need to set the correctCamera off

    }

    public void FixedUpdateState(GameStateManager gameStateManager) { }
    public void UpdateState(GameStateManager gameStateManager) { }
    public void LateUpdateState(GameStateManager gameStateManager) { }


    public void ExitState(GameStateManager gameStateManager) 
    {
        // Disable Ball & AimGuide
        gameStateManager._ballManager.ball.SetActive(false);
        gameStateManager._ballManager.aimGuide.SetActive(false);

        // Disable all UI Panels
        gameStateManager._uIManager.DisableAllUIPanels();
    }

}
