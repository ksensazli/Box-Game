using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NiceSDK
{
    [System.Serializable]
    public class DictionaryPoolTypeListPoolConfig : UnitySerializedDictionary<ePoolType, PoolConfig>
    {
    }
    
    public class PoolManagerBase : MonoBehaviourSingleton<PoolManager>
    {
        [SerializeField]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
        public DictionaryPoolTypeListPoolConfig PoolItemsDefinition = new DictionaryPoolTypeListPoolConfig();
        
        [ReadOnly, Header("Pool Objects")] public DictionaryPoolTypeListGameObject ActiveObjects = new DictionaryPoolTypeListGameObject();
        [ReadOnly] public DictionaryPoolTypeListGameObject AvailableObjects = new DictionaryPoolTypeListGameObject();
        
        //Transforms generated for organization purposes
        [ReadOnly, Header("Pool Holders")] public Transform PoolHolder;
        [ReadOnly] public DictionaryPoolTypeListTransform PoolHolderType = new DictionaryPoolTypeListTransform();

        protected override void OnAwakeEvent()
        {
            base.OnAwakeEvent();

            if (PoolItemsDefinition.Count > 0 && (PoolHolder == null || PoolItemsDefinition.Count != PoolHolder.childCount))
            {
                Prebake();
            }
            else
            {
                CreateFromExistingTransforms();
            }
        }

        public override void Start()
        {
            base.Start();
        }

        protected virtual void OnEnable()
        {
            GameManager.OnGameReset += OnCoreLoop_Reset;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            GameManager.OnGameReset -= OnCoreLoop_Reset;
        }

        public virtual void OnCoreLoop_Reset()
        {
            foreach (var item in ActiveObjects)
            {
                //Debug.LogError(item.Key);
                for (int i = item.Value.List.Count - 1; i >= 0; i--)
                {
                    //Debug.LogError(i);
                    Queue(item.Key, item.Value.List[i]);
                }
            }
        }

        #region Prebake
        [Button]
        private void Prebake()
        {
            if (Application.IsPlaying(this))
                Debug.LogError("Instantiating on Init.");
            RemoveTransforms();
            CreateTransforms();
        }

        private void RemoveTransforms()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            ClearLists();
        }

        private void CreateTransforms()
        {
            PoolHolder = new GameObject("Pool Holder").transform;
            PoolHolder.transform.parent = transform;

            foreach (KeyValuePair<ePoolType, PoolConfig> item in PoolItemsDefinition)
            {
                var dummyTransform = new GameObject(item.Key.ToString()).transform;
                dummyTransform.SetParent(PoolHolder);
                PoolHolderType.Add(item.Key, dummyTransform);

                List<GameObject> m_DummyList = new List<GameObject>();

                for (int i = 0; i < item.Value.InitialQuantity; i++)
                {
                    if (item.Value.Prefab == null)
                    {
                        Debug.LogError("Prefab is Null. " + item.Key);
                        continue;
                    }
                    else
                    {
#if UNITY_EDITOR
                        var dummyGameObject = PrefabUtility.InstantiatePrefab(item.Value.Prefab) as GameObject;
#else
                            var dummyGameObject = Instantiate(item.Value.Prefab);
#endif
                        dummyGameObject.SetActive(false);
                        dummyGameObject.name = item.Key + " Pool " + i;
                        dummyGameObject.transform.SetParent(PoolHolderType[item.Key]);
                        m_DummyList.Add(dummyGameObject);
                    }

                }

                AvailableObjects.Add(item.Key, new GameObjectList(m_DummyList));
                ActiveObjects.Add(item.Key, new GameObjectList());
            }
        }
        #endregion

        #region CreateFrom Existing Transforms
        private void CreateFromExistingTransforms()
        {
            //Debug.LogError("CreateFromExistingTransforms");
            ClearLists();
            RebuildLists();
        }

        private void RebuildLists()
        {
            foreach (KeyValuePair<ePoolType, PoolConfig> item in PoolItemsDefinition)
            {
                var dummyTransform = transform.FindDeepChild<Transform>(item.Key.ToString());
                PoolHolderType.Add(item.Key, dummyTransform);

                List<GameObject> dummyList = new List<GameObject>();

                for (int i = 0; i < dummyTransform.childCount; i++)
                {
                    if (dummyTransform.GetChild(i) != PoolHolder)
                        dummyList.Add(dummyTransform.GetChild(i).gameObject);
                }

                AvailableObjects.Add(item.Key, new GameObjectList(dummyList));
                ActiveObjects.Add(item.Key, new GameObjectList());
            }
        }

        [Button]
        private void ClearLists()
        {
            ActiveObjects.Clear();
            AvailableObjects.Clear();
            PoolHolderType.Clear();
        }
        #endregion


        public GameObject Dequeue(ePoolType type)
        {
            var dummyGameObject = DequeueObject(type);

            dummyGameObject.SetActive(true);

            return dummyGameObject;
        }

        public GameObject Dequeue(ePoolType type, Vector3 position)
        {
            var dummyGameObject = DequeueObject(type);
            dummyGameObject.transform.position = position;
            dummyGameObject.SetActive(true);

            return dummyGameObject;
        }

        private GameObject DequeueObject(ePoolType type)
        {
            GameObject dummyGameObject = null;

            if (AvailableObjects[type].List.Count > 0)
            {
                dummyGameObject = AvailableObjects[type].List[AvailableObjects[type].List.Count - 1];
                AvailableObjects[type].List.Remove(dummyGameObject);
            }
            else
            {
                dummyGameObject = Instantiate(PoolItemsDefinition[type].Prefab);


                if (dummyGameObject == null)
                {
                    Debug.LogError("Prefab is Null. Check Pool Manager dictionary -" + type + " entry.");

                    return null;
                }

                dummyGameObject.name = type + " Pool ";
                dummyGameObject.transform.SetParent(PoolHolderType[type]);
            }

            ActiveObjects[type].List.Add(dummyGameObject);

            return dummyGameObject;
        }

        //TO DO : Check extension method gameobject.Queue(i_type)
        public void Queue(ePoolType type, GameObject objectToQue)
        {
            if (ActiveObjects[type].List.Contains(objectToQue))
                ActiveObjects[type].List.Remove(objectToQue);
            if (AvailableObjects[type].List.Contains(objectToQue))
                return;
            if (objectToQue==null)
            {
               return;
            }
            objectToQue.transform.SetParent(PoolHolderType[type]);
            objectToQue.transform.position = Vector3.zero;
            objectToQue.transform.rotation = Quaternion.identity;

            objectToQue.name = type + " Pool ";
            objectToQue.SetActive(false);
        }
    }
}

