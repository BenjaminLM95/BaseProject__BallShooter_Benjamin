using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class GameStateManager : MonoBehaviour
{
    [Header("Debug (read only)")]
    [SerializeField] private string lastActiveState;
    [SerializeField] private string currentActiveState;

    [Header("Script References")]
    public GameManager _gameManager;
    public LevelManager _levelManager;
    public UIManager _uIManager;
    public BallManager _ballManager;
    public CameraManager _cameraManager;

    // Private variables to store state information
    private IGameState currentGameState;  // Current active state
    private IGameState lastGameState;     // Last active state (kept private for encapsulation)

    // Public getter for accessing the lastGameState externally (read-only access)
    public IGameState LastGameState
    {
        get { return lastGameState; }
    }


    // Instantiate game state objects
    public GameState_GameInit gameState_GameInit = new GameState_GameInit();
    public GameState_MainMenu gameState_MainMenu = new GameState_MainMenu();
    public GameState_Aim gameState_Aim = new GameState_Aim();
    public GameState_Rolling gameState_Rolling = new GameState_Rolling();
    public GameState_LevelComplete gameState_LevelComplete = new GameState_LevelComplete();
    public GameState_LevelFailed gameState_LevelFailed = new GameState_LevelFailed();
    public GameState_Paused gameState_Paused = new GameState_Paused();



    void Awake()
    {
        // Check for missing script references
        if (_gameManager == null) { Debug.LogError("GameManager is not assigned in the Inspector!"); }
        if (_levelManager == null) { Debug.LogError("LevelManager is not assigned in the Inspector!"); }
        if (_uIManager == null) { Debug.LogError("UIManager is not assigned in the Inspector!"); }
        if (_ballManager == null) { Debug.LogError("BallManager is not assigned in the Inspector!"); }
        if (_cameraManager == null) { Debug.LogError("CameraManager is not assigned in the Inspector!"); }

        // Start in the GameInit state when the game is first loaded
        // GameInit is responsible for initializing/resetting the game
        currentGameState = gameState_GameInit;
    }

    #region State Machine Update Calls

    void Start()
    {
        // Enter the initial game state
        currentGameState.EnterState(this);
    }

    // Fixed update is called before update, and is used for physics calculations
    private void FixedUpdate()
    {
        // Handle physics updates in the current active state (if applicable)
        currentGameState.FixedUpdateState(this);
    }

    private void Update()
    {
        // Handle regular frame updates in the current active state
        currentGameState.UpdateState(this);

        // Keeping track of active and last states for debugging purposes
        currentActiveState = currentGameState.ToString();   // Show current state in Inspector
        lastActiveState = lastGameState?.ToString();        // Show last state in Inspector
    }

    // LateUpdate for any updates that need to happen after regular Update
    private void LateUpdate()
    {
        currentGameState.LateUpdateState(this);
    }

    #endregion

    // Method to switch between states
    public void SwitchToState(IGameState newState)
    {
        // Exit the current state (handling cleanup and transitions)
        currentGameState.ExitState(this);

        // Store the current state as the last state before switching
        lastGameState = currentGameState;

        // Switch to the new state
        currentGameState = newState;

        // Enter the new state (initialize any necessary logic)
        currentGameState.EnterState(this);
    }


    // UI Button calls this to resume the game when paused
    public void UnPause()
    {
        if (currentGameState == gameState_Paused)
        {
            if (LastGameState == gameState_Aim)
            {
                SwitchToState(gameState_Aim);
            }

            else if (LastGameState == gameState_Rolling)
            {
                SwitchToState(gameState_Rolling);
            }

        }
    }

}
