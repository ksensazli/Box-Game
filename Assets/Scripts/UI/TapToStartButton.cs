using UnityEngine.UI;

public class TapToStartButton : Button
{
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(OnButtonDown);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveAllListeners();
    }

    private void OnButtonDown()
    {
        GameManager.Instance.StartLevel();
    }
}
