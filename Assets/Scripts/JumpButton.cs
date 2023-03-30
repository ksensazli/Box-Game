using System;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    public static Action<eZoneType> OnJumpButton;
    [SerializeField] private Button _selfButton;
    [SerializeField] private Image _image;
    [SerializeField] private eZoneType _zoneType;
    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OnButtonDown);
        JumperControllerBase.onJumperReset += onJumperReset;

        _image.sprite = GameConfig.Instance.ZoneVariables.ZoneTypeDict[_zoneType].ButtonSprite;
    }

    private void OnDisable()
    {
        JumperControllerBase.onJumperReset -= onJumperReset;
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
        HapticManager.Instance.Haptic(GameConfig.Instance.Haptics.HapticsData[eHapticType.UIButton]);
        SoundManager.Instance.PlaySound(eSFXTypes.ThrowingThud);
        
        _selfButton.interactable = false;
        OnJumpButton?.Invoke(_zoneType);
    }
}
