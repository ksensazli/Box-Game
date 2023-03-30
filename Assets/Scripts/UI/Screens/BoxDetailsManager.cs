using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetailsManager : MonoBehaviour
{
    [Serializable] public class BoxDetailsDict : UnitySerializedDictionary<eZoneType, BoxDetailUI> { };
    public BoxDetailsDict BoxDetails;
    
    private void OnEnable()
    {
        foreach (var VARIABLE in BoxDetails)
        {
           VARIABLE.Value.gameObject.SetActive(LevelManager.Instance.Level.LevelData.IncludedZone.ContainsKey(VARIABLE.Key));
        }
    }
}
