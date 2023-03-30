using System;
using NiceSDK;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static Action OnLevelStarted;
    public static Action OnGameReset;
    public static Action OnLevelCompleted;
    public static Action OnLevelFailed;
    public eGameStates GameState;
    protected override void OnAwakeEvent()
    {
        base.OnAwakeEvent();
        Application.targetFrameRate = 60;
        PlayerPrefManager.Instance.CurrentLevel = PlayerPrefManager.Instance.HighScoreLevel;
    }

    public void StartLevel()
    {
        Debug.Log("Game state changing : "+ eGameStates.Playing);
        if (GameState.Equals(eGameStates.Playing))
        {
            return;
        }
        GameState = eGameStates.Playing;
        OnLevelStarted?.Invoke();
        
        HapticManager.Instance.Haptic(GameConfig.Instance.Haptics.HapticsData[eHapticType.LevelStart]);
    }

    public void ResetGame()
    {
        if (!GameState.Equals(eGameStates.Failed))
        {
            PlayerPrefManager.Instance.CurrentLevel++;
        }
       
        Debug.Log("Game state changing : "+ eGameStates.Idle);
        if (GameState.Equals(eGameStates.Idle))
        {
            return;
        }
        OnGameReset?.Invoke();
        GameState = eGameStates.Idle;
        
        StartLevel();
    }

    public void FailLevel()
    {
        
        
        Debug.Log("Game state changing : "+ eGameStates.Failed);
        if (GameState.Equals(eGameStates.Failed))
        {
            return;
        }
        
        OnLevelFailed?.Invoke();
        GameState = eGameStates.Failed;
        
        HapticManager.Instance.Haptic(GameConfig.Instance.Haptics.HapticsData[eHapticType.LevelFail]);
    }

    public void CompleteLevel()
    {
        Debug.Log("Game state changing : "+ eGameStates.Completed);
        if (GameState.Equals(eGameStates.Completed))
        {
            return;
        }
        OnLevelCompleted?.Invoke();
        GameState = eGameStates.Completed;
        
        SoundManager.Instance.PlaySound(eSFXTypes.Success);
        HapticManager.Instance.Haptic(GameConfig.Instance.Haptics.HapticsData[eHapticType.LevelSuccess]);
    }
}
