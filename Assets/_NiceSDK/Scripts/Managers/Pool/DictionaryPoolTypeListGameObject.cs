using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DictionaryPoolTypeListGameObject : UnitySerializedDictionary<ePoolType, GameObjectList>
{
}

[Serializable]
public class DictionaryPoolTypeHashSetGameObject : UnitySerializedDictionary<ePoolType, GameObjectHashSet>
{
}

[Serializable]
public class DictionaryPoolTypeListTransform : UnitySerializedDictionary<ePoolType, Transform>
{
}

[Serializable]
public class GameObjectList
{
    public List<GameObject> List;

    public GameObjectList()
    {
        List = new List<GameObject>();
    }
    public GameObjectList(List<GameObject> i_List)
    {
        List = i_List;
    }
}

[Serializable]
public class GameObjectHashSet
{
    public HashSet<GameObject> HashSet;

    public GameObjectHashSet()
    {
        HashSet = new HashSet<GameObject>();
    }
    public GameObjectHashSet(HashSet<GameObject> i_HashSet)
    {
        HashSet = i_HashSet;
    }
}