using UnityEngine;
using UnityEngine.UI;

public class BoxDetailUI : MonoBehaviour
{
    [SerializeField] private eZoneType _eZoneType;
    [SerializeField] private TMPro.TMP_Text _collectedAmount;
    [SerializeField] private Image _image;
    private void OnEnable()
    {
        _image.sprite = GameConfig.Instance.ZoneVariables.ZoneTypeDict[_eZoneType].ButtonSprite;
        var targetBox = TargetBoxesManager.Instance.GetTargetBoxController(_eZoneType);
        _collectedAmount.text =targetBox.CollectedBoxAmount + "/ " + targetBox.TargetBoxAmount ;
    }
}
