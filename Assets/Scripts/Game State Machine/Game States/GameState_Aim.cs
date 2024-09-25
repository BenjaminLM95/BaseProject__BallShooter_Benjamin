using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)


public class GameState_Aim : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        Cursor.visible = false;

        gameStateManager._uIManager.UIGamePlay();

        gameStateManager._ballManager.aimGuide.SetActive(true);
        gameStateManager._ballManager.ball.SetActive(true);  
        gameStateManager._cameraManager.UseGameplayCamera();

        gameStateManager._uIManager.modeText.text = "Aim with MOUSE \n & \n Shoot with SPACE";
    }

    public void FixedUpdateState(GameStateManager gameStateManager) { }

    public void UpdateState(GameStateManager gameStateManager)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStateManager._ballManager.ballShoot();
            gameStateManager.SwitchToState(gameStateManager.gameState_Rolling);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameStateManager.SwitchToState(gameStateManager.gameState_Paused);
        }
    }

    public void LateUpdateState(GameStateManager gameStateManager)
    {
        // set aimGuide to match position of the ball
        gameStateManager._ballManager.HandleAimGuide();
    }

    public void ExitState(GameStateManager gameStateManager)
    {
        gameStateManager._ballManager.aimGuide.SetActive(false);
    }
}

