using System;
using DG.Tweening;
using UnityEngine;

public class JumperOnAir : JumperControllerBase
{

    public Transform BoxCenterPos;
    [SerializeField] private Transform _bounceRotation;
    private Tween _falseThrowTween;
    protected override void OnEnable()
    {
        base.OnEnable();
        JumpButton.OnJumpButton += OnJumpButton;
        
        Material[] materials = _meshRenderer.materials;
        materials[0] = GameConfig.Instance.ZoneVariables.ZoneTypeDict[_zoneType].ZoneMaterial;
        materials[1] = GameConfig.Instance.ZoneVariables.ZoneTypeDict[_zoneType].ZoneMaterial;
        materials[2] = GameConfig.Instance.ZoneVariables.ZoneTypeDict[_zoneType].ZoneMaterial;
        _meshRenderer.materials = materials;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        JumpButton.OnJumpButton -= OnJumpButton;
    }

    protected override void OnJumpButton(eZoneType obj)
    {
        KillFalseThrow();
        base.OnJumpButton(obj);
    }

    public void KillFalseThrow()
    {
        _falseThrowTween?.Kill();
    }
    public void CollisionEffect(Action callback)
    {
        DOTween.Kill(_rotationBase);
        _falseThrowTween = DOTween.Sequence()
            .Append(_rotationBase.DOLocalRotate(_bounceRotation.localRotation.eulerAngles,  GameConfig.Instance.JumpersVariables.JumperJumpTween.Duration*2f)
                .SetEase( GameConfig.Instance.JumpersVariables.JumperJumpTween.Ease))
            .Append(_rotationBase.DOLocalRotate(Vector3.zero,  GameConfig.Instance.JumpersVariables.JumperResetTween.Duration * 1.5f)
                .SetEase( GameConfig.Instance.JumpersVariables.JumperResetTween.Ease))
            .AppendCallback(()=>callback?.Invoke());
        
    }
}
