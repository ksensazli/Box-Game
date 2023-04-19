using Sirenix.OdinInspector;
using UnityEngine;

namespace Sdks.Handling
{
    [InlineEditor()]
    public abstract class SdkHandler : ScriptableObject
    {
        public abstract void Initialize();
    }
}