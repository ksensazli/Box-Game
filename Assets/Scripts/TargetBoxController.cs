using UnityEngine;

public class TargetBoxController : MonoBehaviour
{
    public eZoneType ZoneType;
    [SerializeField] private ParticleSystem _collectEffect;
    [SerializeField] private MeshRenderer[] _boxMesh;

    private void OnEnable()
    {
        BoxController.OnBoxCollected += OnBoxCollected;
        foreach (var VARIABLE in _boxMesh)
        {
            VARIABLE.material.color= GameConfig.instance.ZoneVariables.ZoneTypeDict[ZoneType].MainColor;
        }
    }

    private void OnDisable()
    {
        BoxController.OnBoxCollected -= OnBoxCollected;
    }

    private void OnBoxCollected(eZoneType obj)
    {
        if (obj.Equals(ZoneType))
        {
            _collectEffect.Play();
        }
    }
}
