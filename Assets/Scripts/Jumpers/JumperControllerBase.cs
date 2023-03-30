using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumperControllerBase : MonoBehaviour
{
   
   public static Action<eZoneType> onJumperReset;
   [SerializeField] protected Transform _rotationBase;
   [SerializeField] private Transform _targetRotation;
   [SerializeField] protected eZoneType _zoneType;
   [SerializeField] protected MeshRenderer _meshRenderer;
   [SerializeField] public int _index;
   
   public bool IsThrowing;
   public eZoneType ZoneType => _zoneType;

   private List<BoxController> _boxControllers = new List<BoxController>();
   
   public void SetData(eZoneType zoneType)
   {
      _zoneType = zoneType;
      ThrowerZone[] throwerZone = GetComponentsInChildren<ThrowerZone>();
      foreach (var VARIABLE in throwerZone)
      {
         VARIABLE.ZoneType = _zoneType;
      }
      
   }
   protected virtual void OnEnable()
   {
     
   }

   protected virtual void OnDisable()
   {
      
   }

   protected void OnJumpButton(eZoneType obj)
   {
      JumperStart(obj);
   }

   protected void JumperStart(eZoneType obj, bool isForced = false)
   {
      if (obj.Equals(_zoneType) || isForced)
      {
         IsThrowing = true;
         List<BoxController> boxesToThrow = new List<BoxController>();
         for (int i = 0; i < _boxControllers.Count; i++)
         {
            boxesToThrow.Add(_boxControllers[0]);
            _boxControllers.RemoveAt(0);
         }
         
         DOTween.Kill(_rotationBase);

         DOTween.Sequence().Append(_rotationBase.DOLocalRotate(_targetRotation.localRotation.eulerAngles,  GameConfig.Instance.JumpersVariables.JumperJumpTween.Duration)
               .SetEase( GameConfig.Instance.JumpersVariables.JumperJumpTween.Ease))
            .AppendCallback(()=>sendBoxesToTarget(boxesToThrow))
            .AppendInterval( GameConfig.Instance.JumpersVariables.Delay)
            .Append(_rotationBase.DOLocalRotate(Vector3.zero,  GameConfig.Instance.JumpersVariables.JumperResetTween.Duration)
               .SetEase( GameConfig.Instance.JumpersVariables.JumperResetTween.Ease))
            .OnComplete(onJumperResetted);

         for (int i = 0; i < boxesToThrow.Count; i++)
         {
            boxesToThrow[i].stopMovement();
            boxesToThrow[i].transform.parent = _rotationBase;
         }
      }
   }
   private void onJumperResetted()
   {
      IsThrowing = false;
      onJumperReset?.Invoke(_zoneType);
   }

   private void sendBoxesToTarget(List<BoxController> boxesToThrow)
   {
      for (int i = 0; i < boxesToThrow.Count; i++)
      {
         ThrowToTarget(boxesToThrow[i]);
      }
      boxesToThrow.Clear();
   }

   protected virtual void ThrowToTarget(BoxController targetBox)
   {
      
   }

   protected virtual void OnTriggerEnter(Collider other) {
      if(other.CompareTag("box"))
      {
         BoxCollided(other.GetComponent<BoxController>());
      }
   }

   protected virtual void BoxCollided(BoxController boxController)
   {
      _boxControllers.Add(boxController);
   }
   private void OnTriggerExit(Collider other) {
      if(other.CompareTag("box"))
      {
         _boxControllers.Remove(other.GetComponent<BoxController>());
      }
   }
}
