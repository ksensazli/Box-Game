using Facebook.Unity;
using Facebook.Unity.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sdks.Handling
{
    [CreateAssetMenu(fileName = "FacebookSdkHandler", menuName = "SdkHandlers/FacebookSdkHandler")]
    public class FacebookSdkHandler : SdkHandler
    {
        [SerializeField]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        private FacebookSettings settings;

        private float _levelStartTime;

        #region Manager Functions

        public override void Initialize() => FB.Init(InitCallback, OnHideUnity);

        #endregion

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
                Debug.Log("Failed to Initialize the Facebook SDK");
        }
        
        private void OnHideUnity(bool isGameShown) => Time.timeScale = isGameShown ? 1 : 0;
    }
}