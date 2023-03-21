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
  }

  private void OnButtonDown()
  {
    OnJumpButton?.Invoke(_zoneType);
  }
}
