using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScreenInGame : ScreenBase
{
    [Serializable] public class JumpButtonsDictionary : UnitySerializedDictionary<eZoneType, GameObject> { };
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public JumpButtonsDictionary JumpButtons;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var VARIABLE in JumpButtons)
        {
            VARIABLE.Value.SetActive(false);
        }
        foreach (var VARIABLE in LevelManager.Instance.Level.LevelData.IncludedZone)
        {
            JumpButtons[VARIABLE.Key].gameObject.SetActive(true);
        }
    }
}
