using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
        public ZoneTypeVariablesEditor ZoneVariables = new ZoneTypeVariablesEditor();
}

[Serializable]
public class ZoneTypeVariablesEditor
{
        [Serializable] public class ZoneTypeDictionary : UnitySerializedDictionary<eZoneType, ZoneTypeData> { };
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
        public ZoneTypeDictionary ZoneTypeDict;

        [Serializable]
        public class ZoneTypeData
        {
                public Color MainColor;
        }
}