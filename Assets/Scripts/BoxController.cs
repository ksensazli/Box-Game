using System;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class BoxController : MonoBehaviour
{
  [SerializeField] private SplineFollower _splineFollower;
  [SerializeField] private LayerMask _layerMask;
  private RaycastHit dummyHit;
  public eZoneType ZoneType;
  private void OnEnable()
  {
    
  }

  private void OnDisable()
  {
    
  }

  public void stopMovement()
  {
    _splineFollower.enabled = false;
  }

  public void jump()
  {
    transform.parent = null;

    if (Physics.Raycast(transform.position+transform.up, transform.TransformDirection(Vector3.down), out dummyHit, Mathf.Infinity, _layerMask))
    {
      Debug.Log(dummyHit.transform.name);
      var throwerZone = dummyHit.transform.GetComponent<ThrowerZone>();
      if (ZoneType.Equals(throwerZone.ZoneType) && !throwerZone.IsRed)
      {
        transform.DOJump(TargetBoxesManager.Instance.GetTargetBoxController(ZoneType).transform.position,1,1,1);
      }
      else
      {
        transform.DOJump(Vector3.forward * 5,1,1,1);
      }
     
    }
  }

  private void Update() {
    Debug.DrawRay(transform.position+transform.up, transform.TransformDirection(Vector3.down * 5));
  }
  
}
