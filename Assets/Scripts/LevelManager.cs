using System;
using NiceSDK;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    public static Action<int> OnRemainingBoxAmountChanged;

    public LevelVariablesEditor.LevelData CurrentLevelData;
    
    public Level Level;
    private int fullBoxAmount;
    public int RemainingBoxAmount { get; private set; }
    public int RemainingBoxAmountAction { get; private set; }
    
    private void OnEnable()
    {
        GameManager.OnGameReset += OnGameReset;
        GameManager.OnLevelStarted += OnLevelStarted;
        TargetBoxController.OnTargetBoxFull += OnTargetBoxFull;
        CubeSpawner.OnBoxSpawned += OnBoxSpawned;
        BoxController.OnBoxAction += OnBoxaction;
        InitLevel();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.OnGameReset -= OnGameReset;
        GameManager.OnLevelStarted -= OnLevelStarted;
        TargetBoxController.OnTargetBoxFull -= OnTargetBoxFull;
        CubeSpawner.OnBoxSpawned -= OnBoxSpawned;
        BoxController.OnBoxAction -= OnBoxaction;
    }

    private void OnBoxaction()
    {
        RemainingBoxAmountAction--;
        if (RemainingBoxAmountAction.Equals(0) && GameManager.Instance.GameState.Equals(eGameStates.Failed))
        {
            GameManager.Instance.FailLevel();
        }
    }

    private void OnBoxSpawned()
    {
        RemainingBoxAmount--;
        RemainingBoxAmount = Mathf.Clamp(RemainingBoxAmount,0, 1000);
        OnRemainingBoxAmountChanged?.Invoke(RemainingBoxAmount);

        if (RemainingBoxAmount.Equals(0))
        {
            GameManager.Instance.GameState = eGameStates.Failed;
        }
    }

    private void OnTargetBoxFull(eZoneType zoneType)
    {
        fullBoxAmount++;
        if (fullBoxAmount>=CurrentLevelData.IncludedZone.Count)
        {
            GameManager.Instance.CompleteLevel();
        }
    }


    private void OnLevelStarted()
    {
    }

    private void OnGameReset()
    {
        fullBoxAmount = 0;
        if (Level!=null)
        {
            Destroy(Level.gameObject);
        }
        InitLevel();
    }

    private void InitLevel()
    {
        Level = Instantiate( GameConfig.Instance.LevelVariables.Levels[PlayerPrefManager.Instance.CurrentLevelMod], transform);
        CurrentLevelData = Level.LevelData;
        RemainingBoxAmount = Level.LevelData.TotalBoxSpawnAmount;
        RemainingBoxAmountAction = RemainingBoxAmount;
        OnRemainingBoxAmountChanged?.Invoke(RemainingBoxAmount);
    }
    
}
