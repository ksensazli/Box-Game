using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableSingleton<GameConfig>
{
        public ZoneTypeVariablesEditor ZoneVariables = new ZoneTypeVariablesEditor();
        public LevelVariablesEditor LevelVariables = new LevelVariablesEditor();
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

[Serializable]
public class LevelVariablesEditor
{
        public List<LevelData> Levels;
        [Serializable]
        public class LevelData
        {
                public int BoxAmount;
                public int targetBoxAmount;
        }
}