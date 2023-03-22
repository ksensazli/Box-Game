using System;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static Action OnLevelStarted;
    public static Action OnGameReset;
    public static Action OnLevelCompleted;
    public static Action OnLevelFailed;
    public eGameStates GameState;
    
    public static gameManager Instance { get; private set; }

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

    public void levelStart()
    {
        OnLevelStarted?.Invoke();
        GameState = eGameStates.Playing;
    }

    public void gameReset()
    {
        OnGameReset?.Invoke();
        GameState = eGameStates.Idle;
    }

    public void levelFailed()
    {
        OnLevelFailed?.Invoke();
        GameState = eGameStates.Failed;
    }

    public void levelCompleted()
    {
        OnLevelCompleted?.Invoke();
        GameState = eGameStates.Completed;
    }
}
