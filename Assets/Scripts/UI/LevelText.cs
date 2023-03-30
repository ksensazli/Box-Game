using System;
using NiceSDK;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _levelText;

    private void OnEnable()
    {
        GameManager.OnGameReset += OnGameReset;
        UpdateLevelText();
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= OnGameReset;
    }

    private void OnGameReset()
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        _levelText.text = "LEVEL " + (PlayerPrefManager.Instance.CurrentLevel);
    }
}
