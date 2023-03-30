using System.Collections;
using UnityEngine;
using NiceSDK;
using Sirenix.OdinInspector;
using MoreMountains.NiceVibrations;

public class HapticManager : MonoBehaviourSingleton<HapticManager>
{
    [Title("References")]
    [SerializeField, ReadOnly] private GameConfig _gameConfig;

    private bool _isHapticReady = true;

    [Button]
    private void setRef()
    {
        _gameConfig = GameConfig.Instance;
    }
    private void OnValidate()
    {
        setRef();
    }
    public void ToggleEnable()
    {
        PlayerPrefManager.Instance.IsHapticEnabled = !PlayerPrefManager.Instance.IsHapticEnabled;
    }

    public void Haptic(HapticTypes haptic, bool defaultToRegularVibrate = false, bool allowVibrationOnLegacyDevices = true)
    {
        if (PlayerPrefManager.Instance.IsHapticEnabled)
        {
            MMVibrationManager.Haptic(haptic, defaultToRegularVibrate, allowVibrationOnLegacyDevices);
        }
    }
    public void PlayHaptic(eHapticType hapticToPlay)
    {
        if (!_isHapticReady || !_gameConfig.Haptics.HapticsData.ContainsKey(hapticToPlay)) return;
        _isHapticReady = false;
        Haptic(_gameConfig.Haptics.HapticsData[hapticToPlay]);
        StartCoroutine(delayedHaptic());
    }
    private IEnumerator delayedHaptic()
    {
        yield return new WaitForSeconds(_gameConfig.Haptics.DelayBetweenHaptics);
        _isHapticReady = true;
    }
}
