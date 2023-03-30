using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelBaker : MonoBehaviour
{
    public LevelVariablesEditor.LevelData levelData;

    [Button]
    private void SetRefs()
    {
        //levelSceneData.ZoneTypes.Clear();
        // JumperDefault[] jumperDefaults = GetComponentsInChildren<JumperDefault>();
        // for (int i = 0; i < jumperDefaults.Length; i++)
        // {
        //     LevelVariablesEditor.JumperData jumperData = new LevelVariablesEditor.JumperData();
        //     jumperData.Position = jumperDefaults[i].transform.position;
        //     jumperData.ZoneType = jumperDefaults[i].ZoneType;
        //     levelSceneData.Jumpers.Add(jumperData);
        // }
        //
        levelData.Splines.Clear();

        CubeSpawner[] cubeSpawners = GetComponentsInChildren<CubeSpawner>();
        for (int i = 0; i < cubeSpawners.Length; i++)
        {
            LevelVariablesEditor.LevelSplineData levelSplineData = new LevelVariablesEditor.LevelSplineData();
            levelSplineData.IncludedZoneTypes = new List<eZoneType>();
            JumperDefault[] jumperDefaultChilds = cubeSpawners[i].transform.GetComponentsInChildren<JumperDefault>(true);
            for (int j = 0; j < jumperDefaultChilds.Length; j++)
            {
                levelSplineData.IncludedZoneTypes.Add(jumperDefaultChilds[j].ZoneType);    
            }
            levelData.Splines.Add(levelSplineData);
        }
    }

}
