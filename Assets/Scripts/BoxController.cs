using System;
using UnityEngine;

public class BoxController : MonoBehaviour
{
  private RaycastHit dummyHit;
  private void OnEnable()
  {
    JumpButton.OnJumpButton += OnJumpButton;
  }

  private void OnDisable()
  {
    JumpButton.OnJumpButton -= OnJumpButton;
  }

  private void OnJumpButton(eZoneType zoneType)
  {
    
    if (Physics.Raycast(transform.position+Vector3.up, transform.TransformDirection(Vector3.down), out dummyHit, Mathf.Infinity))
    {
      if (zoneType.Equals( dummyHit.transform.GetComponent<ThrowerZone>().ZoneType))
      {
        Debug.LogError("yoyoyo");
      }
     
    }
  }
  
}
