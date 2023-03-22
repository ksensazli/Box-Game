using System;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    public static Action<eZoneType> OnJumpButton;
    [SerializeField] private Button _selfButton;
    [SerializeField] private eZoneType _zoneType;
    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OnButtonDown);
        JumperController.onJumperReset += onJumperReset;

        _selfButton.image.color = GameConfig.instance.ZoneVariables.ZoneTypeDict[_zoneType].MainColor;
    }

    private void OnDisable()
    {
        JumperController.onJumperReset -= onJumperReset;
    }

    private void onJumperReset(eZoneType obj)
    {
        if (obj.Equals(_zoneType))
        {
            _selfButton.interactable = true;
        }
    }

    private void OnButtonDown()
    {
        _selfButton.interactable = false;
        OnJumpButton?.Invoke(_zoneType);
    }
}
