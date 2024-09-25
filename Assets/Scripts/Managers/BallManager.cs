using System.Collections;
using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class BallManager : MonoBehaviour
{
    [Header("References")]
    public GameObject ball;  // Ball Mesh?
    public Rigidbody rb_ball;
    public GameObject aimGuide;

    [Header("Script References")]
    public UIManager _uIManager;
    public GameManager _gameManager;
    public GameStateManager _gameStateManager;
    public CameraManager _cameraManager;


    public bool ballStopped;
    public float ballMagnitudeStopThreshold = 0.1f; // Adjust this value as needed
    public float ballStopCheckDelay = 0.5f; // Adjust this value as needed

    [SerializeField, Header("Debug Output (read only)")]
    private float ballVelocityMagnitude;

    public void Awake()
    {
        // Check for missing script references
        if (_uIManager == null) { Debug.LogError("UIManager is not assigned to BallManager in the Inspector!"); }
        if (_gameManager == null) { Debug.LogError("GameManager is not assigned to BallManager in the Inspector!"); }
        if (_gameStateManager == null) { Debug.LogError("GameStateManager is not assigned to BallManager in the Inspector!"); }
        if (_cameraManager == null) { Debug.LogError("CameraManager is not assigned to BallManager in the Inspector!"); }
    }



    private void Start()
    {
        ballStopped = true; // the ball should be stopped at the start of the game
    }

 
    void Update()
    {
        // spits out the ball vector magnitude for debugging    
        ballVelocityMagnitude = rb_ball.velocity.magnitude;
    }

    public void ballShoot() // adds force to ball in a direction away from camera
    {
        _gameManager.shotsLeft -= 1;
        _uIManager.UpdateShotsleft(_gameManager.shotsLeft);

        ballStopped = false; // the ball should be moving at this point
        rb_ball.AddForce(aimGuide.transform.forward * 25, ForceMode.VelocityChange);
    }

    public IEnumerator CheckBallStoppedAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(ballStopCheckDelay);

        // Continuously check if the ball has stopped moving
        while (true)
        {
            if (rb_ball.velocity.magnitude < ballMagnitudeStopThreshold)
            {
                StopBall(); // Stop the ball
                ballStopped = true;
                yield break; // Exit the coroutine as the check is complete
            }

            yield return null; // Wait until the next frame and check again
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoalTrigger")
        {
            Debug.Log("Goal Reached");
            _gameStateManager.SwitchToState(_gameStateManager.gameState_LevelComplete);
            return;
        }

        else if (other.gameObject.tag == "ResetTrigger") 
        {
            SetBallToStartPosition();
        }
    }

    public void HandleAimGuide()
    {
        // Get the direction the camera is facing, constrained to the Y-axis only
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;  // Ensure no vertical rotation is applied (Y-axis locked)

        // Calculate the new rotation that matches the camera's facing direction
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

        // Smoothly interpolate between the current rotation and the target rotation
        aimGuide.transform.rotation = Quaternion.Slerp(aimGuide.transform.rotation, targetRotation, Time.deltaTime * 50f);
    }

    public void SetBallToStartPosition()
    {
        // Find Start Position object in current scene
        Transform startPosition = GameObject.FindWithTag("BallStartPosition").transform;

        StopBall(); // Stop the ball   
        rb_ball.position = startPosition.transform.position;
        rb_ball.rotation = startPosition.transform.rotation;


        // ** Bugfix by Daniel Nascimento **
        // When setting position and rotation in the rigidbody
        // it'll take effect during the physics update. So I believe
        // when cinemachine tries to get the rotation to adjust the camera
        // the rotation hasn't changed yet. So I'm setting it in the
        // transform here as well.
        rb_ball.transform.position = startPosition.transform.position;
        rb_ball.transform.rotation = startPosition.transform.rotation;
    }




    public void StopBall() //immediately halts the ball movement
    {
        rb_ball.isKinematic = true;
        rb_ball.isKinematic = false;
        // Also setting velocities to zero just in case.
        rb_ball.velocity = Vector3.zero;
        rb_ball.angularVelocity = Vector3.zero;
    }
}
