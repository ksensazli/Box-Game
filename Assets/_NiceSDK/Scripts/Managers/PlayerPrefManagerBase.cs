using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NiceSDK
{
    public class PlayerPrefManagerBase : MonoBehaviourSingleton<PlayerPrefManager>
    {
        
        
        [PropertyOrder(-1), ShowInInspector] public int DefaultStartlevel = 1;

        [ShowInInspector, PropertyOrder(0)] 
        public bool IsSFXMuted { get { return PlayerPrefs.GetInt(nameof(IsSFXMuted), 0) == 1; } set { PlayerPrefs.SetInt(nameof(IsSFXMuted), value == true ? 1 : 0); } }
        [ShowInInspector, PropertyOrder(0)]
        public bool IsMusicMuted { get { return PlayerPrefs.GetInt(nameof(IsMusicMuted), 0) == 1; } set { PlayerPrefs.SetInt(nameof(IsMusicMuted), value == true ? 1 : 0);} }
             
        [ShowInInspector, PropertyOrder(0)] 
        public bool IsHapticEnabled { get { return PlayerPrefs.GetInt(nameof(IsHapticEnabled), 1) == 1; } set { PlayerPrefs.SetInt(nameof(IsHapticEnabled), value == true ? 1 : 0); } }

        
        private int m_CurrentLevel = 0;
        [PropertyOrder(-1), ShowInInspector]
        public int CurrentLevel
        {
            get
            {
                return m_CurrentLevel;
            }
            set
            {
                if (value != m_CurrentLevel) m_CurrentLevel = value;
                if (m_CurrentLevel > HighScoreLevel) HighScoreLevel = m_CurrentLevel;
            }
        }

        public int CurrentLevelMod => (CurrentLevel-1) % GameConfig.Instance.LevelVariables.Levels.Count;

        [PropertyOrder(-1), ShowInInspector]
        public int HighScoreLevel
        {
            get
            {
                return PlayerPrefs.GetInt(nameof(HighScoreLevel), DefaultStartlevel);
            }
            protected set
            {
                PlayerPrefs.SetInt(nameof(HighScoreLevel), value);
            }
        }

        [ShowInInspector]
        public int CoinsAmount
        {
            get
            {
                return PlayerPrefs.GetInt(nameof(CoinsAmount), 0);
            }
            set
            {
                PlayerPrefs.SetInt(nameof(CoinsAmount), value);
                OnCoinsAmountChanged?.Invoke(value);
            }
        }
        
        public delegate void CoinsAmountChangedEvent(int i_CoinsAmount);
        public static event CoinsAmountChangedEvent OnCoinsAmountChanged = delegate { };


        [Button, PropertyOrder(-20)]
        public void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
