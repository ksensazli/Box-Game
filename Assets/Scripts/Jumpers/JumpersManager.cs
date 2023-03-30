using System.Linq;
using NiceSDK;
using UnityEngine;

public class JumpersManager : MonoBehaviourSingleton<JumpersManager>
{

   [SerializeField] private JumperControllerBase[]  _totalAirJumpers;
   [SerializeField] private JumperOnAir[] _airJumpers;
   [SerializeField] private JumperAutomatic[] _automaticJumpers;

   private void OnEnable()
   {
      GameManager.OnLevelStarted += OnLevelStarted;
   }

   public override void OnDisable()
   {
      base.OnDisable();
      GameManager.OnLevelStarted -= OnLevelStarted;
   }

   private void OnLevelStarted()
   {
      LevelVariablesEditor.LevelData level = LevelManager.Instance.Level.LevelData;
      _airJumpers = level.JumpersOnAir;
      _automaticJumpers = level.JumpersAutomatic;

      _totalAirJumpers = _airJumpers.Concat<JumperControllerBase>(_automaticJumpers).ToArray();
      for (int i = 0; i < _totalAirJumpers.Length; i++)
      {
         _totalAirJumpers[i]._index = i;
      }
   }

   public bool HasMoreJumperOnAir(int index) => _totalAirJumpers.Length > index;
   public Transform GetAirJumper(int index)
   {
      return _totalAirJumpers[index].transform;
   }
}
