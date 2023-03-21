using System.Collections.Generic;
using UnityEngine;

public class TargetBoxesManager : MonoBehaviour
{
    public static TargetBoxesManager Instance { get; private set; }
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

    public List<ThrowerZone> BoxControllers;

    public ThrowerZone GetTargetBoxController(eZoneType zoneType)
    {
        foreach (var VARIABLE in BoxControllers)
        {
            if (VARIABLE.ZoneType.Equals(zoneType))
            {
                return VARIABLE;
            }
        }

        return null;
    }
    
}
