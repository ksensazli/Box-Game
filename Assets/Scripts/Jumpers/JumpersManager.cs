using System.Collections.Generic;
using UnityEngine;

public class JumpersManager : MonoBehaviour
{
   public static JumpersManager Instance { get; private set; }
   private void Awake() 
   {
      if (Instance != null && Instance != this) 
      { 
         Destroy(this); 
      } 
      else 
      { 
         Instance = this; 
      } 
   }

   [SerializeField] private List<JumperControllerBase> _defaultJumpers;
   [SerializeField] private List<JumperOnAir> _airJumpers;


   public Transform GetAirJumper()
   {
      return _airJumpers[UnityEngine.Random.Range(0, _airJumpers.Count)].BoxCenterPos.transform;
   }
}
