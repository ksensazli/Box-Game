using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCompleted : ScreenBase
{
   [SerializeField] private Button _continueButton;
    
   protected override void OnEnable()
   {
       base.OnEnable();
       _continueButton.onClick.AddListener(OnContinueButton);
   }

   private void OnDisable()
   {
        _continueButton.onClick.RemoveAllListeners();
   }

   private void OnContinueButton()
   {
       //claim RV
        GameManager.Instance.ResetGame();
   }
}
