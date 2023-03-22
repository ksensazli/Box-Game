using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAmountHUD : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _boxAmountText;
    private int _collectedBoxAmount;
    private int _targetBoxAmount;

    private void OnEnable()
    {
        _targetBoxAmount = GameConfig.instance.LevelVariables.Levels[0].targetBoxAmount;
        _collectedBoxAmount = 0;
        
        updateBoxAmountText();

        BoxController.OnBoxCollected += OnBoxCollected;
    }

    private void OnDisable()
    {
        BoxController.OnBoxCollected -= OnBoxCollected;
    }

    private void OnBoxCollected(eZoneType obj)
    {
        _collectedBoxAmount++;
        updateBoxAmountText();
        if (_collectedBoxAmount >= _targetBoxAmount)
        {
            gameManager.Instance.levelCompleted();
        }
    }

    private void updateBoxAmountText()
    {
        _boxAmountText.text = _collectedBoxAmount + " / " + _targetBoxAmount;
    }
}
