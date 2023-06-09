using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AirJumperTrigger : MonoBehaviour
{
   [SerializeField] private JumperOnAir _jumperControllerBase;
   private BoxController _boxController;
   private List<BoxController> _boxes = new List<BoxController>();

   private void OnEnable()
   {
      _jumperControllerBase.OnThrow += OnThrow;
   }

   private void OnDisable()
   {
      _jumperControllerBase.OnThrow -= OnThrow;
   }

   private void OnThrow()
   {
      if (_boxController == null)
      {
         return;
      }
      _boxController.jump(true);
      _boxController = null;
      _jumperControllerBase.KillFalseThrow();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(nameof(eTags.box)))
      {
         var boxController = other.GetComponent<BoxController>();
         if (_boxes.Contains(boxController))
         {
            return;
         }
         _boxController = boxController;
         _boxes.Add(boxController);
         if (boxController.AirJumperIndex -1 > _jumperControllerBase._index)
         {
            return;
         }
         if (_jumperControllerBase.IsThrowing)
         {
            boxController.jump(true);
            _jumperControllerBase.KillFalseThrow();
         }
         else
         {
            boxController.SetParentToJumper(transform);
            _jumperControllerBase.CollisionEffect(()=>Throw(boxController));
            boxController.stopMovement();
         }
      }
   }
   
   private void Throw(BoxController box)
   {
      box.FallFromAir(transform.up);
      _boxController = null;
   }
}
