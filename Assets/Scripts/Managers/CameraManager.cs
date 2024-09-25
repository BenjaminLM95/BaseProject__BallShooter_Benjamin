using UnityEngine;
using Cinemachine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class CameraManager : MonoBehaviour
{
    public GameObject VCamGameplay;
    public GameObject VCamMainMenu;

    public CinemachineBrain cinemachineBrain;

    public CinemachineFreeLook freeLookCamera;
    public Transform lookAtTarget;

    public Vector3 cameraOffset = new Vector3(0, 5, -10);



    public void UseMainMenuCamera()
    {
        VCamGameplay.SetActive(false);
        VCamMainMenu.SetActive(true);
    }

    public void UseGameplayCamera()
    {
        VCamGameplay.SetActive(true);
        VCamMainMenu.SetActive(false);
    }

    public void DisableCameraRotation()
    {
        VCamGameplay.SetActive(false); }

    public void EnableCameraRotation()
    {
        VCamGameplay.SetActive(true);
    }

    // set gameplay camera to a target orientation
    public void SetGameplayCameraOrientation(Vector3 targetOrientation)
    {
        VCamGameplay.transform.LookAt(targetOrientation);
    }


   
    public void ResetCameraPosition()
    {
        Debug.Log("camera_reset");
        
        //cinemachineBrain.enabled = false;// an attempt to hide the repositioning.    

        var offset = freeLookCamera.LookAt.rotation * new Vector3(0, 0, -14);

        freeLookCamera.ForceCameraPosition(freeLookCamera.LookAt.position + offset, freeLookCamera.LookAt.rotation);
        freeLookCamera.m_YAxis.Value = 0.5f;

        //cinemachineBrain.enabled = true;// an attempt to hide the repositioning.  
    }





}
