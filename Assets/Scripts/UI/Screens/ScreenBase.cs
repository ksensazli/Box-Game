using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScreenBase : MonoBehaviour
{
   [SerializeField] protected CanvasGroup _canvasGroup;
   public bool CloseOtherScreens;

   [Button]
   protected virtual void SetRef()
   {
      _canvasGroup = GetComponentInChildren<CanvasGroup>(true);
   }
   
   protected virtual void OnEnable()
   {
      Open();
   }
   
   

   public virtual void Open()
   {
      DOTween.Kill(_canvasGroup);
      _canvasGroup.DOFade(1, GameConfig.Instance.MenuVariables.MenuFadeInTween.Duration)
         .SetEase(GameConfig.Instance.MenuVariables.MenuFadeInTween.Ease)
         .From(0);

   }
   public virtual void Close()
   {
      DOTween.Kill(_canvasGroup);
      _canvasGroup.DOFade(0, GameConfig.Instance.MenuVariables.MenuFadeOutTween.Duration)
         .SetEase(GameConfig.Instance.MenuVariables.MenuFadeOutTween.Ease)
         .OnComplete(()=>gameObject.SetActive(false));
   }
}
