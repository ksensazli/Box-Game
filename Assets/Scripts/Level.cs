using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<CubeSpawner> _cubeSpawners;
    public LevelVariablesEditor.LevelData LevelData;

    [Button]
    private void SetRefs()
    {
        _cubeSpawners.Clear();
        CubeSpawner[] cubeSpawners = GetComponentsInChildren<CubeSpawner>();

        for (int i = 0; i < cubeSpawners.Length; i++)
        {
            _cubeSpawners.Add(cubeSpawners[i]);    
        }
        
        //levelSceneData.ZoneTypes.Clear();
        // JumperDefault[] jumperDefaults = GetComponentsInChildren<JumperDefault>();
        // for (int i = 0; i < jumperDefaults.Length; i++)
        // {
        //     LevelVariablesEditor.JumperData jumperData = new LevelVariablesEditor.JumperData();
        //     jumperData.Position = jumperDefaults[i].transform.position;
        //     jumperData.ZoneType = jumperDefaults[i].ZoneType;
        //     levelSceneData.Jumpers.Add(jumperData);
        // }

        foreach (var VARIABLE in LevelData.IncludedZone)
        {
            for (int i = 0; i < VARIABLE.Value.Jumpers.Count; i++)
            {
                VARIABLE.Value.Jumpers[i].SetData(VARIABLE.Key);
            }
        }

        for (int i = 0; i < cubeSpawners.Length; i++)
        {
            LevelVariablesEditor.LevelSplineData levelSplineData = new LevelVariablesEditor.LevelSplineData();
            levelSplineData.Spawner = cubeSpawners[i];
            levelSplineData.IncludedZoneTypes = new List<eZoneType>();
            JumperDefault[] jumperDefaultChilds = cubeSpawners[i].transform.GetComponentsInChildren<JumperDefault>(true);
            for (int j = 0; j < jumperDefaultChilds.Length; j++)
            {
                levelSplineData.IncludedZoneTypes.Add(jumperDefaultChilds[j].ZoneType);    
            }
          
            if (LevelData.Splines.Count > i)
            {
                levelSplineData.CubeMovementSpeed = LevelData.Splines[i].CubeMovementSpeed;
                levelSplineData.CubeSpawnPeriod = LevelData.Splines[i].CubeSpawnPeriod;
                LevelData.Splines[i] = levelSplineData;
            }
            else
            {
                LevelData.Splines.Add(levelSplineData);
            }
            
        }
        
        LevelData.JumpersOnAir = GetComponentsInChildren<JumperOnAir>();
        LevelData.JumpersAutomatic = GetComponentsInChildren<JumperAutomatic>();
    }


    private void OnEnable()
    {
        for (int i = 0; i < _cubeSpawners.Count; i++)
        {
            _cubeSpawners[i].Init(LevelData.Splines[i]);
        }
    }
}
