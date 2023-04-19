using AppsFlyerSDK;
using UnityEngine;

namespace Sdks.Handling
{
    [CreateAssetMenu(fileName = "AppsFlyerSdkHandler", menuName = "SdkHandlers/AppsFlyerSdkHandler")]
    public class AppsFlyerSdkHandler : SdkHandler
    {
        public string devKey;
        public string appID;
        public string UWPAppID;
        public string macOSAppID;
        public bool isDebug;

        #region Unity Events

        public override void Initialize()
        {
            AppsFlyer.setIsDebug(isDebug);

#if UNITY_WSA_10_0 && !UNITY_EDITOR
                AppsFlyer.initSDK(devKey, UWPAppID, getConversionData ? this : null);
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
                AppsFlyer.initSDK(devKey, macOSAppID, getConversionData ? this : null);
#else
            AppsFlyer.initSDK(devKey, appID, null);
#endif
            AppsFlyer.startSDK();
        }

        #endregion
}
}