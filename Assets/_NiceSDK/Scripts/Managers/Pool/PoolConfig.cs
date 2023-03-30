using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace NiceSDK
{
    [Serializable]
    public class PoolConfig
    {
         public GameObject Prefab;
        public int InitialQuantity;
    }
}
