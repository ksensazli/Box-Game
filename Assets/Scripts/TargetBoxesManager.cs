using System;
using NiceSDK;
using Sirenix.OdinInspector;
using UnityEngine;

public class TargetBoxesManager : MonoBehaviourSingleton<TargetBoxesManager>
{
    [Serializable] public class TargetBoxesDict : UnitySerializedDictionary<eZoneType, TargetBoxController> { };

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)] 
    [SerializeField] private GameObject _floor;
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
        var isTopDown = LevelManager.Instance.CurrentLevelData.IsTopDown;
        _floor.gameObject.SetActive(!isTopDown);
        foreach (var VARIABLE in TargetBoxes)
        {
            VARIABLE.Value.gameObject.SetActive(false);
        }

        foreach (var VARIABLE in LevelManager.Instance.CurrentLevelData.IncludedZone)
        {
            TargetBoxes[VARIABLE.Key].gameObject.SetActive(true);
        }
        //TO DO CHECK THIS MESS
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
                isTopDown ? 5f :  TargetBoxes[eZoneType.Type1].transform.position.z);
            TargetBoxes[eZoneType.Type2].transform.position = new Vector3(
                2,
                TargetBoxes[eZoneType.Type2].transform.position.y,
                isTopDown ? 5f : TargetBoxes[eZoneType.Type2].transform.position.z);
        }
        else
        {
            TargetBoxes[eZoneType.Type1].transform.position = new Vector3(
                -2.5f,
                TargetBoxes[eZoneType.Type1].transform.position.y,
                TargetBoxes[eZoneType.Type1].transform.position.z);
            TargetBoxes[eZoneType.Type2].transform.position = new Vector3(
                0,
                TargetBoxes[eZoneType.Type2].transform.position.y,
                TargetBoxes[eZoneType.Type2].transform.position.z);
            TargetBoxes[eZoneType.Type3].transform.position = new Vector3(
                2.5f,
                TargetBoxes[eZoneType.Type3].transform.position.y,
                TargetBoxes[eZoneType.Type3].transform.position.z);
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
