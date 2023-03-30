using System;
using UnityEngine;

public class RemainingBoxesUI : MonoBehaviour
{
   [SerializeField] private TMPro.TMP_Text _remainingText;

   private void OnEnable()
   {
      LevelManager.OnRemainingBoxAmountChanged += OnRemainingBoxAmountChanged;
      _remainingText.text = LevelManager.Instance.RemainingBoxAmount.ToString();
   }

   private void OnDisable()
   {
      LevelManager.OnRemainingBoxAmountChanged -= OnRemainingBoxAmountChanged;
   }

   private void OnRemainingBoxAmountChanged(int obj)
   {
      _remainingText.text = obj.ToString();
   }
}
