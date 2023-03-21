using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumperController : MonoBehaviour
{
   public static Action<eZoneType> onJumperReset;
   [SerializeField] private Transform _rotationBase;
   [SerializeField] private Transform _targetRotation;
   [SerializeField] private eZoneType _zoneType;
   private List<BoxController> _boxControllers = new List<BoxController>();
   

   private void OnEnable()
   {
      JumpButton.OnJumpButton += OnJumpButton;
   }

   private void OnDisable()
   {
      JumpButton.OnJumpButton -= OnJumpButton;
   }

   private void OnJumpButton(eZoneType obj)
   {
      if (obj.Equals(_zoneType))
      {
         DOTween.Kill(_rotationBase);

         DOTween.Sequence().Append(_rotationBase.DOLocalRotate(_targetRotation.localRotation.eulerAngles, .125f)
            .SetEase(Ease.OutSine))
            .AppendCallback(sendBoxesToTarget)
            .AppendInterval(.05f)
            .Append(_rotationBase.DOLocalRotate(Vector3.zero, .075f)
            .SetEase(Ease.OutSine))
            .OnComplete(onJumperResetted);

         for (int i = 0; i < _boxControllers.Count; i++)
         {
            _boxControllers[i].stopMovement();
            _boxControllers[i].transform.parent = _rotationBase;
         }
      }
   }

   private void onJumperResetted()
   {
      onJumperReset?.Invoke(_zoneType);
   }

   private void sendBoxesToTarget()
   {
      for (int i = 0; i < _boxControllers.Count; i++)
         {
            _boxControllers[i].jump();
         }
      _boxControllers.Clear();
   }

   private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("box"))
      {
         _boxControllers.Add(other.GetComponent<BoxController>());
      }
   }

   private void OnTriggerExit(Collider other) {
      if(other.CompareTag("box"))
      {
         _boxControllers.Remove(other.GetComponent<BoxController>());
      }
   }
}
