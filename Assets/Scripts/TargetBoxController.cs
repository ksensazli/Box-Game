using System;
using UnityEngine;

public class TargetBoxController : MonoBehaviour
{
    public static Action<eZoneType> OnTargetBoxFull;
    
    public eZoneType ZoneType;
    [SerializeField] private ParticleSystem _collectEffect;
    [SerializeField] private MeshRenderer[] _boxMesh;
    [SerializeField] private TMPro.TMP_Text _targetText;
    [SerializeField] private Collider _collectCollider;
    [SerializeField] private Animator _targetBoxAnimator;
    [SerializeField] private ParticleSystem _completeEffect;
    [SerializeField] private ParticleSystem _zoneEffect;
    [SerializeField] private Bouncer _bouncer;
    public int CollectedBoxAmount { get; private set; }
    public int TargetBoxAmount { get; private set; }
    
    

    private void OnEnable()
    {
        transform.ScaleUp();
        BoxController.OnBoxCollected += OnBoxCollected;
        GameManager.OnGameReset += OnGameReset;
        GameManager.OnLevelFailed += OnLevelFailed;

        _bouncer.ZoneType = ZoneType;
        foreach (var VARIABLE in _boxMesh)
        {
            VARIABLE.material = GameConfig.Instance.ZoneVariables.ZoneTypeDict[ZoneType].ZoneMaterial;
        }
        
        _collectCollider.enabled = true;
        _targetText.gameObject.SetActive(true);
        TargetBoxAmount = LevelManager.Instance.CurrentLevelData.IncludedZone[ZoneType]
            .RequiredBoxAmount;
        CollectedBoxAmount = 0;
        UpdateTargetText();
        _zoneEffect.Play();
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= OnGameReset;
        BoxController.OnBoxCollected -= OnBoxCollected;
        GameManager.OnLevelFailed -= OnLevelFailed;
    }

    private void OnLevelFailed()
    {
        _collectCollider.enabled = false;
        _zoneEffect.Stop();
        _targetText.gameObject.SetActive(false);
    }

    private void OnGameReset()
    {
        _targetBoxAnimator.SetTrigger(AnimIDs.ReOpenID);
    }

    private void OnBoxCollected(eZoneType obj)
    {
        if (obj != ZoneType) return;
        
        CollectedBoxAmount++;

        if (CollectedBoxAmount >= TargetBoxAmount)
        {
            BoxFilled();
        }
        UpdateTargetText();
        _collectEffect.Play();
    }

    private void BoxFilled()
    {
        OnTargetBoxFull?.Invoke(ZoneType);
        _collectCollider.enabled = false;
        _completeEffect.Play();
        _targetBoxAnimator.SetTrigger(AnimIDs.WinEffectID);
        SoundManager.Instance.PlaySound(eSFXTypes.DuctTapeBox);
        _zoneEffect.Stop();
        _targetText.gameObject.SetActive(false);
    }
    private void UpdateTargetText()
    {
        _targetText.text =CollectedBoxAmount  + "/" + TargetBoxAmount;
    }
}
