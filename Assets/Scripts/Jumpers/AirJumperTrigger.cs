using UnityEngine;

public class AirJumperTrigger : MonoBehaviour
{
   [SerializeField] private JumperOnAir _jumperControllerBase;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(nameof(eTags.box)))
      {
         var boxController = other.GetComponent<BoxController>();
         if (boxController.AirJumperIndex -1 > _jumperControllerBase._index)
         {
            return;
         }
         if (_jumperControllerBase.IsThrowing)
         {
            boxController.jump(true);
         }
         else
         {
            boxController.FallFromAir(transform.up);
            _jumperControllerBase.CollisionEffect();
         }
      }
   }
}
