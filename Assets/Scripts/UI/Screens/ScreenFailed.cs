using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFailed : ScreenBase
{
    [SerializeField] private Button _retryButton;

    protected override void OnEnable()
    {
        base.OnEnable();
        _retryButton.onClick.AddListener(onRetryButton);
    }

    private void OnDisable()
    {
        _retryButton.onClick.RemoveAllListeners();
    }

    private void onRetryButton()
    {
        GameManager.Instance.ResetGame();
    }
}
