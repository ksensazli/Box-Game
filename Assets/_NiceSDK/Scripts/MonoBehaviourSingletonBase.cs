using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceSDK;

namespace NiceSDK
{
    public abstract class MonoBehaviourSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected virtual void Awake()
        {
        }
    }
}