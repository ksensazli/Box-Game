using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class TargetBoxController : MonoBehaviour
{
    public eZoneType ZoneType;
    [SerializeField] private ParticleSystem _collectEffect;
    [SerializeField] private MeshRenderer[] _boxMesh;
    [SerializeField] private List<BoxPanelData> _boxPanelData;
    [Serializable]
    public class BoxPanelData
    {
        public Transform BoxPanel;
        public Transform BoxPanelTargetRotation;
    }
    

    private void OnEnable()
    {
        BoxController.OnBoxCollected += OnBoxCollected;
        gameManager.OnLevelCompleted += OnLevelCompleted;
        foreach (var VARIABLE in _boxMesh)
        {
            VARIABLE.material.color= GameConfig.instance.ZoneVariables.ZoneTypeDict[ZoneType].MainColor;
        }
    }

    private void OnDisable()
    {
        BoxController.OnBoxCollected -= OnBoxCollected;
        gameManager.OnLevelCompleted -= OnLevelCompleted;
    }

    private void OnBoxCollected(eZoneType obj)
    {
        if (obj.Equals(ZoneType))
        {
            _collectEffect.Play();
        }
    }
    
    private void OnLevelCompleted()
    {
        for (int i = 0; i < _boxPanelData.Count; i++)
        {
            int index = i;
            _boxPanelData[index].BoxPanel
                .DOLocalRotate(_boxPanelData[index].BoxPanelTargetRotation.localRotation.eulerAngles, .75f, RotateMode.LocalAxisAdd)
                .SetDelay(index * .25f);
        }
    }
}
