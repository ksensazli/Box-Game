using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using NiceSDK;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    public static Action OnBoxSpawned;
    [SerializeField] private SplineComputer _splineComputer;
    [SerializeField] private PathMaterialMover _materialMover;
    
    private LevelVariablesEditor.LevelSplineData _levelSplineData;
    private List<eZoneType> remainingZone = new List<eZoneType>();
    private bool _removing;
    private IEnumerator spawnRoutine;
    private void OnEnable()
    {
        GameManager.OnLevelStarted += OnLevelStarted;
        TargetBoxController.OnTargetBoxFull += OnTargetBoxFull;
    }

    private void OnDisable()
    {
        GameManager.OnLevelStarted -= OnLevelStarted;
        TargetBoxController.OnTargetBoxFull -= OnTargetBoxFull;
    }



    public void Init(LevelVariablesEditor.LevelSplineData levelSplineData)
    {
        _levelSplineData = levelSplineData;
        _materialMover.Speed = _levelSplineData.CubeMovementSpeed;
    }

    private void OnLevelStarted()
    {
        for (int i = 0; i <   _levelSplineData.IncludedZoneTypes.Count; i++)
        {
            remainingZone.Add( _levelSplineData.IncludedZoneTypes[i]);
        }

        spawnRoutine = StartToSpawnCube();
        StartCoroutine(spawnRoutine);
        
    }
    private void OnTargetBoxFull(eZoneType obj)
    {
        _removing = true;
        remainingZone.Remove(obj);
        _removing = false;
    }
    private IEnumerator StartToSpawnCube()
    {
        while(GameManager.Instance.GameState.Equals(eGameStates.Playing))
        {
            if (_removing)
            {
                yield return null;
            }
            else
            {
                if (remainingZone.Count.Equals(0))
                {
                    break;
                }
                var dummyCube = PoolManager.Instance.Dequeue(ePoolType.Box).GetComponent<BoxController>();
                int targetType = UnityEngine.Random.Range(0, remainingZone.Count);
                dummyCube.Init(_splineComputer,_levelSplineData.CubeMovementSpeed,
                    remainingZone[targetType]);  
                OnBoxSpawned?.Invoke();
                yield return new WaitForSeconds(_levelSplineData.CubeSpawnPeriod);
            }
        }
    }
}
