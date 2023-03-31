using System;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using NiceSDK;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : SingletonScriptableObject<GameConfig>
{
        public ZoneTypeVariablesEditor ZoneVariables = new ZoneTypeVariablesEditor();
        public LevelVariablesEditor LevelVariables = new LevelVariablesEditor();
        public JumperVariablesEditor JumpersVariables = new JumperVariablesEditor();
        public BoxVariablesEditor BoxVariables = new BoxVariablesEditor();
        public DefaultTweenVariablesEditor DefaultTweenVariables = new DefaultTweenVariablesEditor();
        public MenuVariablesEditor MenuVariables = new MenuVariablesEditor();
        public HapticVariablesEditor Haptics = new HapticVariablesEditor();
        public SoundVariablesEditor Sound = new SoundVariablesEditor();
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
                //[OnValueChanged("@this.ZoneMaterial.color == this.MainColor")]
                [OnValueChanged("@this.ZoneMaterial.color = this.MainColor")] public Color MainColor;
                [PreviewField(100,ObjectFieldAlignment.Left)]public Material ZoneMaterial;
                public Sprite ButtonSprite;
        }
}

[Serializable]
public class LevelVariablesEditor
{
        public List<Level> Levels;

        [Serializable]
        public class LevelData
        {
                public bool HasJumperOnAir => (JumpersOnAir.Length + JumpersAutomatic.Length) > 0;
                public bool IsTopDown => LevelCameraType.Equals(CameraManager.eCameras.TopDownLevel);
                public CameraManager.eCameras LevelCameraType;
                public int TotalBoxSpawnAmount;
                public List<LevelSplineData> Splines;
                

                [Serializable,BoxGroup, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)] 
                public class ZonesDictionary : UnitySerializedDictionary<eZoneType, TypeContainer> { };
                public ZonesDictionary IncludedZone;
                public JumperOnAir[] JumpersOnAir;
                public JumperAutomatic[] JumpersAutomatic;
        }

        [Serializable]
        public class TypeContainer
        {
                public int RequiredBoxAmount;
                public List<JumperControllerBase> Jumpers;
        }
        
        [Serializable]
        public class LevelSplineData
        {
                [ReadOnly] public CubeSpawner Spawner;
                public float CubeSpawnPeriod;
                public float CubeMovementSpeed;
                public bool IsReverted;
                [ReadOnly] public List<eZoneType> IncludedZoneTypes;
        }
}

[Serializable]
public class JumperVariablesEditor
{
        public DefaultTween JumperJumpTween;
        public float Delay;
        public DefaultTween JumperResetTween;
}
 
[Serializable]
public struct DefaultTween
{
        public float Duration;
        public Ease Ease;
}

[Serializable]
public class BoxVariablesEditor
{
        public DefaultTween DefaultJumpTween;
        public DefaultTween DefaultToAirJumpTween;
        public DefaultTween AirToBoxJumpTween;
}

[Serializable]
public class DefaultTweenVariablesEditor
{
        public DefaultTween DefaultScaleUpTween;
        public DefaultTween DefaultScaleDownTween;
        public DefaultTween DefaultFadeTween;
}

[Serializable]
public class MenuVariablesEditor
{
        public DefaultTween MenuFadeInTween;
        public DefaultTween MenuFadeOutTween;
        public float DelayToLevelCompleteScreen;
}

[Serializable]
public class HapticVariablesEditor
{
        [Serializable] public class HapticsDictionary : UnitySerializedDictionary<eHapticType, HapticTypes> { };
        public HapticsDictionary HapticsData;

        public float DelayBetweenHaptics = .1f;
}

[Serializable]
public class SoundVariablesEditor
{
        [Serializable,BoxGroup, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
        public class SoundDictionary : UnitySerializedDictionary<eSFXTypes, SoundVariables> { };
        public SoundDictionary SoundsDict;
        [Serializable]
        public class SoundVariables
        {
                public AudioClip Clip;
                [Range(0,3)] public float Volume = 1;
                [Range(0,3)] public float Pitch = 1;
        }
}