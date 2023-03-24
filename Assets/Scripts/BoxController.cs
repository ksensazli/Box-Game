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
  [SerializeField] private MeshRenderer _meshRenderer;

  private RaycastHit dummyHit;
  public eZoneType ZoneType;
  
  private void OnEnable()
  {
    //Material
    _meshRenderer.materials[1].color = GameConfig.instance.ZoneVariables.ZoneTypeDict[ZoneType].MainColor;
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
    _rigidBody.useGravity = false;
    _rigidBody.velocity = Vector3.zero;
  }

  public void jump(bool isJumperOnAir)
  {
    bool hasJumperOnAir = GameConfig.instance.LevelVariables.Levels[0].HasJumperOnAir;
    transform.parent = null;

    if (Physics.Raycast(transform.position+transform.up, transform.TransformDirection(Vector3.down), out dummyHit, Mathf.Infinity, _layerMask))
    {
      var throwerZone = dummyHit.transform.GetComponent<ThrowerZone>();
 
      //transform.DORotate(new Vector3(360,0,0),1f,RotateMode.WorldAxisAdd);

      if (ZoneType.Equals(throwerZone.ZoneType) && !throwerZone.IsRed || isJumperOnAir)
      {
       // _rigidBody.useGravity = true;
       if (hasJumperOnAir && !isJumperOnAir)
        {
          ThrowToAir();
        }
        else
        {
          ThrowToBox();
        }
        return;
      }
    }
    else if(isJumperOnAir)
    {
      ThrowAirToBox();
      return;
    }
    FalseThrow();
  }

  private void ThrowToBox()
  {
    var targetPos = TargetBoxesManager.Instance.GetTargetBoxController(ZoneType).transform.position;
    transform.DOJump(targetPos,2,1,1)
      .OnComplete(()=>activateRigidBody(true));
  }

  private void ThrowToAir()
  {
    var targetPos = JumpersManager.Instance.GetAirJumper().transform.position;
    transform.DOMove(targetPos, GameConfig.instance.BoxVariables.DefaultToAirJumpTween.Duration)
      .SetEase(GameConfig.instance.BoxVariables.DefaultToAirJumpTween.Ease)
      .OnComplete(FallFromAir);
  }

  private void FallFromAir()
  {
    _rigidBody.useGravity = true;
    _rigidBody.AddForce((new Vector3(UnityEngine.Random.Range(.1f,.5f),-1f,.25f)) * 5, ForceMode.Impulse);
  }
  
  private void ThrowAirToBox()
  {
    var targetPos = TargetBoxesManager.Instance.GetTargetBoxController(ZoneType).transform.position;
    transform.DOMove(targetPos, GameConfig.instance.BoxVariables.AirToBoxJumpTween.Duration)
      .SetEase(GameConfig.instance.BoxVariables.AirToBoxJumpTween.Ease);
  }
  private void FalseThrow()
  {
    transform.DOJump(TargetBoxesManager.Instance.GetTargetBoxController(ZoneType)
        .transform.position + new Vector3(UnityEngine.Random.Range(-2,2),1.5f,UnityEngine.Random.Range(1.5f,2)),2,1,1)
      .OnComplete(()=>activateRigidBody(false));
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
