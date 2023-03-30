using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceSDK;

namespace NiceSDK
{
    public class MonoBehaviourSingleton<T> : MonoBehaviourSingletonBase<MonoBehaviourSingleton<T>> where T : MonoBehaviour
    {
        private static T _instance;

        public bool DontDestroyLoad;

        protected bool _isDestroyed = false;

        protected static bool applicationIsQuittingFlag = false;
        protected static bool applicationIsQuitting = false;

        public static bool IsInstanceNull { get { return _instance == null; } }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        if (applicationIsQuitting && Application.isPlaying)
                        {
                            return _instance;
                        }
                        else
                        {                            
                            //GameObject singleton = new GameObject();
                            //_instance = singleton.AddComponent<T>();
                            //singleton.name = "[Singleton] " + typeof(T).ToString();
                        }
                    }
                }

                return _instance;
            }

        }

        protected sealed override void Awake()
        {
            if (_instance == null)
            {
                _instance = gameObject.GetComponent<T>();
                if (DontDestroyLoad)
                {
                    setDontDestroyOnLoad();
                }
                OnAwakeEvent();
            }
            else
            {
                if (this == _instance)
                {
                    if (DontDestroyLoad)
                    {
                        setDontDestroyOnLoad();
                    }
                    OnAwakeEvent();
                }
                else
                {
                    _isDestroyed = true;
                    Destroy(this.gameObject);
                }
            }
        }

        protected virtual void OnAwakeEvent() { }

        public virtual void Start() { }

        public virtual void OnDisable()
        {
        }

        public virtual void OnDestroy()
        {
        }

        protected void setDontDestroyOnLoad()
        {
            DontDestroyLoad = true;
            if (DontDestroyLoad)
            {
                if (transform.parent != null)
                {
                    transform.parent = null;
                }
                DontDestroyOnLoad(gameObject);
            }
        }
#if UNITY_EDITOR
        public virtual void OnApplicationQuit()
        {
            applicationIsQuittingFlag = true;
        }
#endif
    }
}
