using UnityEngine;

public class TargetBoxController : MonoBehaviour
{
    public eZoneType ZoneType;
    [SerializeField] private ParticleSystem _collectEffect;

    private void OnEnable()
    {
        BoxController.OnBoxCollected += OnBoxCollected;
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
