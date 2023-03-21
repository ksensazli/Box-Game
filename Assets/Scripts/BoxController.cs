using System;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class BoxController : MonoBehaviour
{
  [SerializeField] private SplineFollower _splineFollower;
  [SerializeField] private LayerMask _layerMask;
  [SerializeField] private Rigidbody _rigidBody;
  [SerializeField] private BoxCollider _boxCollider;
  private RaycastHit dummyHit;
  public eZoneType ZoneType;
  private void OnEnable()
  {
    
  }

  private void OnDisable()
  {
    
  }

  public void Init(SplineComputer splineComputer)
  {
    _splineFollower.spline = splineComputer;
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
      transform.DORotate(new Vector3(360,0,0),1f,RotateMode.WorldAxisAdd);
      if (ZoneType.Equals(throwerZone.ZoneType) && !throwerZone.IsRed)
      {
        transform.DOJump(TargetBoxesManager.Instance.GetTargetBoxController(ZoneType)
        .transform.position + new Vector3(UnityEngine.Random.Range(-0.75f,0.75f),1f,UnityEngine.Random.Range(-0.75f,0.75f)),2,1,1)
        .OnComplete(activateRigidBody);
      }
      else
      {
        transform.DOJump(Vector3.forward * 5,1,1,1);
      }
     
    }
  }

  private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("boxWall"))
    {
      DOVirtual.DelayedCall(.3f,activateCollider);
    }
  }

  private void activateCollider()
  {
    _boxCollider.isTrigger = false;
  }

  private void activateRigidBody()
  {
    _rigidBody.useGravity = true;
  }
}
