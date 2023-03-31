using System;
using Cinemachine;
using NiceSDK;
using UnityEngine;

public class CameraManager : MonoBehaviourSingleton<CameraManager>
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [Serializable] public class CameraDict : UnitySerializedDictionary<eCameras, CinemachineVirtualCamera> { }
    [SerializeField] private CameraDict _cameras;
    public Camera MainCamera;
    public enum eCameras
    {
        RegularLevel,
        TopDownLevel,
        FarLevel
    }
    
    public void SwitchCamera(eCameras i_eCameraToEnable, bool i_IsInstant = false)
    {
        disableAll();

        if (i_IsInstant)
        {
            _cinemachineBrain.enabled = false;
        }

        _cameras[i_eCameraToEnable].enabled = true;

        _cinemachineBrain.enabled = true;
    }
    private void disableAll()
    {
        foreach (var VARIABLE in _cameras)
        {
            VARIABLE.Value.enabled = false;
        }
    }
}
