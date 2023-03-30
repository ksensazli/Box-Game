using System;
using NiceSDK;
using Sirenix.OdinInspector;
using UnityEngine;

public class TargetBoxesManager : MonoBehaviourSingleton<TargetBoxesManager>
{
    [Serializable] public class TargetBoxesDict : UnitySerializedDictionary<eZoneType, TargetBoxController> { };
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public TargetBoxesDict TargetBoxes;
    
    private void OnEnable()
    {
        GameManager.OnLevelStarted += OnLevelStarted;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.OnLevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        var includedTypes = LevelManager.Instance.CurrentLevelData.IncludedZone.Count;

        foreach (var VARIABLE in TargetBoxes)
        {
            VARIABLE.Value.gameObject.SetActive(false);
        }

        foreach (var VARIABLE in LevelManager.Instance.CurrentLevelData.IncludedZone)
        {
            TargetBoxes[VARIABLE.Key].gameObject.SetActive(true);
        }
        //TO DO CHECK THIS
        if (includedTypes.Equals(1))
        {
            TargetBoxes[eZoneType.Type1].transform.position = new Vector3(
                0,
                TargetBoxes[eZoneType.Type1].transform.position.y,
                TargetBoxes[eZoneType.Type1].transform.position.z);
        }
        else if (includedTypes.Equals(2))
        {
            TargetBoxes[eZoneType.Type1].transform.position = new Vector3(
                -2,
                TargetBoxes[eZoneType.Type1].transform.position.y,
                TargetBoxes[eZoneType.Type1].transform.position.z);
            TargetBoxes[eZoneType.Type2].transform.position = new Vector3(
                2,
                TargetBoxes[eZoneType.Type2].transform.position.y,
                TargetBoxes[eZoneType.Type2].transform.position.z);
        }
        else
        {
            
        }
    }

    public TargetBoxController GetTargetBoxController(eZoneType zoneType)
    {
        foreach (var VARIABLE in TargetBoxes)
        {
            if (VARIABLE.Key.Equals(zoneType))
            {
                return VARIABLE.Value;
            }
        }

        return null;
    }
    
}
