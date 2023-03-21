using System;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class BoxController : MonoBehaviour
{
  public static Action<eZoneType> OnBoxCollected;
  
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
 
      var throwerZone = dummyHit.transform.GetComponent<ThrowerZone>();
 
      transform.DORotate(new Vector3(360,0,0),1f,RotateMode.WorldAxisAdd);

      if (ZoneType.Equals(throwerZone.ZoneType) && !throwerZone.IsRed)
      {
        _rigidBody.useGravity = true;
        var targetPos = TargetBoxesManager.Instance.GetTargetBoxController(ZoneType)
          .transform.position + new Vector3(UnityEngine.Random.Range(-0.75f, 0.75f), 1f,
          UnityEngine.Random.Range(-0.75f, 0.75f));
         transform.DOJump(targetPos,2,1,1)
         .OnComplete(()=>activateRigidBody(true));
     
      
      }
      else
      {
        transform.DOJump(TargetBoxesManager.Instance.GetTargetBoxController(ZoneType)
          .transform.position + new Vector3(UnityEngine.Random.Range(-2,2),1.5f,UnityEngine.Random.Range(1.5f,2)),2,1,1)
          .OnComplete(()=>activateRigidBody(false));
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

  private void activateRigidBody(bool isInZone)
  {
    if (isInZone)
    {
      OnBoxCollected?.Invoke(ZoneType);  
    }
    

    _rigidBody.useGravity = true;
    _rigidBody.AddForce(Vector3.down*.25f);
  }
}
