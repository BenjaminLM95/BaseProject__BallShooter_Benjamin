using UnityEngine;
using UnityEngine.SceneManagement;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)


public class GameState_LevelComplete : IGameState
{  
    public void EnterState(GameStateManager gameStateManager)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        // Lock the camera's rotation to prevent further movement.
        gameStateManager._cameraManager.DisableCameraRotation();

        // Check if the current level is the last level by comparing the build index 
        // of the active scene with the total number of scenes in the build settings.
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // If this is the last level, show the "Game Complete" UI.
            gameStateManager._uIManager.UIGameComplete();
        }
        else
        {
            // If this is not the last level, show the "Level Complete" UI.
            gameStateManager._uIManager.UILevelComplete();
        }
    }


    public void FixedUpdateState(GameStateManager gameStateManager) { }

    public void UpdateState(GameStateManager gameStateManager) { }

    public void LateUpdateState(GameStateManager gameStateManager) { }

    public void ExitState(GameStateManager gameStateManager)
    {
        // Unlock the camera's rotation to allow movement in the next state.  **** shouldnt we just do this in the enter state of the next state?
        gameStateManager._cameraManager.EnableCameraRotation();
    }
}