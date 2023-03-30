using UnityEngine;

public class JumperDefault : JumperControllerBase
{
    
    protected override void OnEnable()
    {
        base.OnEnable();
        JumpButton.OnJumpButton += OnJumpButton;
        Material[] materials = _meshRenderer.materials;

        materials[0] = _meshRenderer.materials[0];
        materials[1] = _meshRenderer.materials[1];
        materials[2] = GameConfig.Instance.ZoneVariables.ZoneTypeDict[_zoneType].ZoneMaterial;
        _meshRenderer.materials = materials;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        JumpButton.OnJumpButton -= OnJumpButton;
    }
    protected override void ThrowToTarget(BoxController targetBox)
    {
        base.ThrowToTarget(targetBox);
        targetBox.jump(false);
    }
}

