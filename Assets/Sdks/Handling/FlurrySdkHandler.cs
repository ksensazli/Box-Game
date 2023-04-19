using FlurrySDK;
using UnityEngine;

namespace Sdks.Handling
{
    [CreateAssetMenu(fileName = "FlurrySdkHandler", menuName = "SdkHandlers/FlurrySdkHandler")]
    public class FlurrySdkHandler : SdkHandler
    {
        [SerializeField]
        private string apiKey = "P7CVTSBMKCR24KHWVZH9";

        private float _levelStartTime;

        #region Unity Events

        public override void Initialize()
        {
            new Flurry.Builder().WithCrashReporting().WithLogEnabled().Build(apiKey);
        }

        #endregion
    }
}