using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class GameManager : MonoBehaviour
{
    // with the refactor GameManager Doesn't really manager anything anymore...
    // it does hold the ShotsLeft varialble but thats it..


    [Header("Gameplay Info")]
    public int shotsLeft = 0;

    [Header("Per Level Info")]
    public LevelInfo _levelInfo;
    public GameObject startPosition;


    [Header("Script References")]
    public GameStateManager _gameStateManager;
    public BallManager _ballManager;
    public LevelManager _levelManager;
    public UIManager _uIManager;
    public CameraManager _cameraManager;

    private void Awake()
    {

        // Contribution: Daniel Nascimento
        // If any of the managers are null, attempt to assign them with children of this object.
        _gameStateManager ??= GetComponentInChildren<GameStateManager>();
        _ballManager ??= GetComponentInChildren<BallManager>();
        _levelManager ??= GetComponentInChildren<LevelManager>();
        _uIManager ??= GetComponentInChildren<UIManager>();
        _cameraManager ??= GetComponentInChildren<CameraManager>();
    }







}
