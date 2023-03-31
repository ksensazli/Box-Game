using System;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;
using NiceSDK;

public class BoxController : MonoBehaviour
{
  public static Action<eZoneType> OnBoxCollected;
  
  [SerializeField] private SplineFollower _splineFollower;
  [SerializeField] private LayerMask _layerMask;
  [SerializeField] private Rigidbody _rigidBody;
  [SerializeField] private BoxCollider _boxCollider;
  [SerializeField] private MeshRenderer _meshRenderer;

  private bool _isThrown;
  private bool _isCorrectThrow;
  private bool _isDirect;
  private bool _isCollected;
  private RaycastHit dummyHit;
  
  public eZoneType ZoneType;
  public int AirJumperIndex = 0;
  
  private void OnEnable()
  {
    GameManager.OnLevelCompleted += OnLevelCompleted;
    GameManager.OnLevelFailed += OnLevelFailed;
    TargetBoxController.OnTargetBoxFull += OnTargetBoxFull;
    _splineFollower.onEndReached += OnEndReached;
  }

  private void OnDisable()
  {
    TargetBoxController.OnTargetBoxFull -= OnTargetBoxFull;
    _splineFollower.onEndReached -= OnEndReached;
    GameManager.OnLevelCompleted -= OnLevelCompleted;
    GameManager.OnLevelFailed -= OnLevelFailed;
  }

  private void OnLevelFailed()
  {
    stopMovement();
    ReturnToPool();
  }

  private void OnTargetBoxFull(eZoneType obj)
  {
    if (obj == ZoneType)
    {
      transform.ScaleDown(()=>PoolManager.Instance.Queue(ePoolType.Box,gameObject));
    }
  }


  private void OnEndReached(double obj)
  {
    _splineFollower.onEndReached -= OnEndReached;
    ReturnToPool();
  }

  private void OnLevelCompleted()
  {
    if (!_isThrown)
    {
      stopMovement();
      ReturnToPool();
      // transform.DOScale(Vector3.zero, GameConfig.instance.DefaultTweenVariables.DefaultScaleDownTween.Duration)
      //   .SetEase(GameConfig.instance.DefaultTweenVariables.DefaultScaleDownTween.Ease)
      //   .OnComplete(() => PoolManager.Instance.Queue(ePoolType.Box,gameObject));
    }
  }

  private void ReturnToPool()
  {
    transform.ScaleDown(()=>PoolManager.Instance.Queue(ePoolType.Box,gameObject));
  }

  public void Init(SplineComputer splineComputer, float speed, eZoneType zoneType = eZoneType.Type1)
  {
    _boxCollider.isTrigger = true;
    _splineFollower.spline = splineComputer;
    _splineFollower.followSpeed = speed;
    ZoneType = zoneType;
    Material[] materials = _meshRenderer.materials;
    materials[1] = GameConfig.Instance.ZoneVariables.ZoneTypeDict[ZoneType].ZoneMaterial;
    _meshRenderer.materials = materials;
    transform.ScaleUp();
    AirJumperIndex = 0;
  }

  public void stopMovement()
  {
    _splineFollower.enabled = false;
    _rigidBody.useGravity = false;
    _rigidBody.velocity = Vector3.zero;
    DOTween.Kill(transform);
  }

  public void SetParentToJumper(Transform parent)
  {
    transform.parent = parent;
  }

  private void Update()
  {
    if (!_isCollected)
    {
      return;
    }

    _rigidBody.position = new Vector3(_rigidBody.position.x, Mathf.Clamp(_rigidBody.position.y, 0, 200),
      _rigidBody.position.z);
  }

  public void jump(bool isJumperOnAir)
  {
    _boxCollider.enabled = false;
    _isThrown = true;
    bool hasJumperOnAir = LevelManager.Instance.Level.LevelData.HasJumperOnAir;
    transform.parent = null;
    _rigidBody.velocity = Vector3.zero;
    _rigidBody.useGravity = true;

    DOVirtual.DelayedCall(.15f, ()=>_boxCollider.enabled = true);
    
    if (Physics.Raycast(transform.position+transform.up, transform.TransformDirection(Vector3.down), out dummyHit, Mathf.Infinity, _layerMask))
    {
      var throwerZone = dummyHit.transform.GetComponent<ThrowerZone>();
 
      
      SoundManager.Instance.PlaySound(eSFXTypes.FlyingWoosh);
      if (ZoneType.Equals(throwerZone.ZoneType) && !throwerZone.IsRed || isJumperOnAir)
      {
        transform.GetChild(0).DORotate(new Vector3(360,0,0),1f,RotateMode.WorldAxisAdd);
         // _rigidBody.useGravity = true;
         if (hasJumperOnAir && !isJumperOnAir)
         {
            ThrowToAir(60); 
         }
         else
         {
           _isDirect = true;
            ThrowToBox(); 
         }
         return;
      }
    }
    else if(isJumperOnAir)
    {
      transform.GetChild(0).DORotate(new Vector3(360,0,0),1f,RotateMode.WorldAxisAdd);
      if (JumpersManager.Instance.HasMoreJumperOnAir(AirJumperIndex))
      {
        ThrowToAir(20);
      }
      else
      {
        ThrowToBox();
      }
  
      return;
    }
    transform.GetChild(0).DORotate(new Vector3(360,0,0),1f,RotateMode.WorldAxisAdd);
    FalseThrow();
  }

  private void ThrowToBox()
  {
    _isCorrectThrow = true;
    _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

    var offset = new Vector3(UnityEngine.Random.Range(-.75f,.75f), .5f, UnityEngine.Random.Range(-.75f,.75f));
    var targetPos = TargetBoxesManager.Instance.GetTargetBoxController(ZoneType).transform.position 
                    + offset;
    
    //Debug.LogError(TargetBoxesManager.Instance.GetTargetBoxController(ZoneType).transform.position +"  "+ offset);
    _rigidBody.velocity
      =  TKExtensions.CalculateVelocity(_rigidBody.position,targetPos,60);
  }
 

  private void ThrowToAir(float rotation)
  {

    _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
//    Debug.LogError(JumpersManager.Instance.GetAirJumper().transform.position);
    // _rigidBody
    //   .AddForce(TKExtensions.CalculateVelocity(_rigidBody.position, JumpersManager.Instance.GetAirJumper(AirJumperIndex++).transform.position,rotation) * _rigidBody.mass, ForceMode.Impulse);
    _rigidBody.velocity 
      = TKExtensions.CalculateVelocity(_rigidBody.position, JumpersManager.Instance.GetAirJumper(AirJumperIndex++).transform.position,rotation);
  }

  public void FallFromAir(Vector3 direction)
  {
    transform.parent = null;
    _rigidBody.useGravity = true;
    _rigidBody.velocity = (direction * 4) ;
    DOVirtual.DelayedCall(5,ReturnToPool);
  }
  
  // private void ThrowAirToBox()
  // {
  //   _isCorrectThrow = true;
  //   _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
  //   var targetPos = TargetBoxesManager.Instance.GetTargetBoxController(ZoneType).transform.position;
  //   transform.DOMove(targetPos, GameConfig.instance.BoxVariables.AirToBoxJumpTween.Duration)
  //     .SetEase(GameConfig.instance.BoxVariables.AirToBoxJumpTween.Ease);
  // }
  private void FalseThrow()
  {
    _rigidBody.velocity = TKExtensions.CalculateVelocity(_rigidBody.position,TargetBoxesManager.Instance.GetTargetBoxController(ZoneType)
      .transform.position + new Vector3(UnityEngine.Random.Range(-2,2),1.5f,UnityEngine.Random.Range(1.5f,2)),60);
    DOVirtual.DelayedCall(1, () => SoundManager.Instance.PlaySound(eSFXTypes.FailBox));
    DOVirtual.DelayedCall(5,ReturnToPool);
  }
  
  private void OnTriggerExit(Collider other)
  {
    //Debug.LogError(other.transform.tag+ "   " + _isCorrectThrow);
    if(other.CompareTag(nameof(eTags.boxWall)) && _isCorrectThrow)
    {
      Invoke("activateCollider",_isDirect ? 0 : .35f);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag(nameof(eTags.CollectionBox)) && !_isCollected && _isCorrectThrow)
    {
      SoundManager.Instance.PlaySound(eSFXTypes.SuccessBox);
      _isCollected = true;
      OnBoxCollected?.Invoke(ZoneType); 
    }
  }

  private void activateCollider()
  {
    _boxCollider.isTrigger = false;
  }
}
