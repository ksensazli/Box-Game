using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumperControllerBase : MonoBehaviour
{
   
   public static Action<eZoneType> onJumperReset;
   [SerializeField] private Transform _rotationBase;
   [SerializeField] private Transform _targetRotation;
   [SerializeField] private eZoneType _zoneType;
   [SerializeField] private MeshRenderer _meshRenderer;
   [SerializeField] private bool _isJumperOnAir;
   private List<BoxController> _boxControllers = new List<BoxController>();
   

   private void OnEnable()
   {
      JumpButton.OnJumpButton += OnJumpButton;
      _meshRenderer.materials[2].color = GameConfig.instance.ZoneVariables.ZoneTypeDict[_zoneType].MainColor;
   }

   private void OnDisable()
   {
      JumpButton.OnJumpButton -= OnJumpButton;
   }

   private void OnJumpButton(eZoneType obj)
   {
      if (obj.Equals(_zoneType))
      {
         List<BoxController> boxesToThrow = new List<BoxController>();
         for (int i = 0; i < _boxControllers.Count; i++)
         {
            boxesToThrow.Add(_boxControllers[0]);
            _boxControllers.RemoveAt(0);
         }
         
         DOTween.Kill(_rotationBase);

         DOTween.Sequence().Append(_rotationBase.DOLocalRotate(_targetRotation.localRotation.eulerAngles,  GameConfig.instance.JumpersVariables.JumperJumpTween.Duration)
            .SetEase( GameConfig.instance.JumpersVariables.JumperJumpTween.Ease))
            .AppendCallback(()=>sendBoxesToTarget(boxesToThrow))
            .AppendInterval( GameConfig.instance.JumpersVariables.Delay)
            .Append(_rotationBase.DOLocalRotate(Vector3.zero,  GameConfig.instance.JumpersVariables.JumperResetTween.Duration)
            .SetEase( GameConfig.instance.JumpersVariables.JumperResetTween.Ease))
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
      onJumperReset?.Invoke(_zoneType);
   }

   private void sendBoxesToTarget(List<BoxController> boxesToThrow)
   {
      for (int i = 0; i < boxesToThrow.Count; i++)
      {
         boxesToThrow[i].jump(_isJumperOnAir);
      }
      boxesToThrow.Clear();
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
