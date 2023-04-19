using UnityEngine;

namespace Sdks.Handling
{
    public class SdkManager : MonoBehaviour
    {
        #region References

        [SerializeField]
        private SdkHandler[] sdkHandlers;

        #endregion

        private void Start()
        {
            foreach (var handler in sdkHandlers)
            {
                if (handler == null) continue;
                Debug.Log(handler.name);
                handler.Initialize();
            }
        }
    }
}