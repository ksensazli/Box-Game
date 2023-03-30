using Sirenix.OdinInspector;
using UnityEngine;

public class SoundManager : SoundManagerBase
{  
    [Title("References")]
    [SerializeField, ReadOnly] private GameConfig _gameConfig;

    [Button]
    private void setRef()
    {
        _gameConfig = GameConfig.Instance;
    }
    private void OnValidate()
    {
        setRef();
    }


   

    public void StopInstant()
    {
        SFXGameAudioSource.Stop();
    }

    public void PlaySFXCustom(AudioClip clip, float volume)
    {
        SFXGameAudioSource.volume = volume;
        SFXGameAudioSource.pitch = 1;
        PlaySFX(clip);
    }
    public void PlaySound(eSFXTypes sfxToPlay)
    {
        if (!_gameConfig.Sound.SoundsDict.ContainsKey(sfxToPlay)) return;
        if (_gameConfig.Sound.SoundsDict[sfxToPlay].Clip == null) return;
    
        var soundToPlay = _gameConfig.Sound.SoundsDict[sfxToPlay];
        SFXGameAudioSource.volume = soundToPlay.Volume;
        SFXGameAudioSource.pitch = soundToPlay.Pitch;
        PlaySFX(soundToPlay.Clip);
    }

    public void PlaySound(SoundVariablesEditor.SoundVariables soundVariables)
    {
        if (soundVariables == null || soundVariables.Clip == null)
        {
            return;
        }
        SFXGameAudioSource.volume = soundVariables.Volume;
        SFXGameAudioSource.pitch = soundVariables.Pitch;
        PlaySFX(soundVariables.Clip);
    }
}
