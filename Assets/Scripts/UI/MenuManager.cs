using System;
using DG.Tweening;
using NiceSDK;

public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    [Serializable] public class ScreenDictionary : UnitySerializedDictionary<eScreens, ScreenBase> { };
    public ScreenDictionary ScreenDict;

    private void OnEnable()
    {
        GameManager.OnLevelStarted += OnLevelStarted;
        GameManager.OnLevelCompleted += OnLevelCompleted;
        GameManager.OnGameReset += OnGameReset;
        GameManager.OnLevelFailed += OnLevelFailed;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.OnLevelStarted -= OnLevelStarted;
        GameManager.OnLevelCompleted -= OnLevelCompleted;
        GameManager.OnGameReset -= OnGameReset;
        GameManager.OnLevelFailed -= OnLevelFailed;
    }

    private void OnLevelFailed()
    {
        OpenMenuScreen(eScreens.Failed);
    }

    private void OnGameReset()
    {
     //   OpenMenuScreen(eScreens.Start);
    }

    private void OnLevelCompleted()
    {
        DOVirtual.DelayedCall(GameConfig.Instance.MenuVariables.DelayToLevelCompleteScreen, () => OpenMenuScreen(eScreens.Completed));
    }

    private void OnLevelStarted()
    {
        OpenMenuScreen(eScreens.InGame);
    }

    public void CloseAllScreens()
    {
        foreach (var VARIABLE in ScreenDict)
        {
            VARIABLE.Value.Close();
        }
    }

    public void OpenMenuScreen(eScreens screen)
    {
        
        ScreenDict[screen].gameObject.SetActive(true);

        if (ScreenDict[screen].CloseOtherScreens)
        {
            foreach (var VARIABLE in ScreenDict)
            {
                if (!VARIABLE.Key.Equals(screen))
                {
                    VARIABLE.Value.Close();
                }
            }
        }
       
    }

    public void CloseMenuScreen(eScreens screen)
    {
        ScreenDict[screen].Close();
    }
}
