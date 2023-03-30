using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiceSDK
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    //Try 'Resources.FindObjectsOfTypeAll'
                    T[] objects = Resources.FindObjectsOfTypeAll<T>();

                    if (objects == null || objects.Length == 0)
                    {
                        //Failed, try 'Resources.LoadAll'
                        objects = Resources.LoadAll<T>(string.Empty);
                    }

                    if (objects.Length > 0 && objects[0] != null)
                    {
                        _instance = objects[0];
                    }

                    if (objects.Length > 1)
                    {
                        Debug.LogError("[SingletonScriptableObject] There is more than 1 object of type - " + typeof(T).FullName + ". Choosing first.");
                    }
                    else if (objects.Length == 0)
                    {
                        Debug.LogError("[SingletonScriptableObject] Can't find the object - " + typeof(T).FullName + ". Make sure it's on the Resources folder.");
                    }
                }

                return _instance;
            }
        }
    }
}
