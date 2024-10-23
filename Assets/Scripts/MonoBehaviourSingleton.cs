using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;



    protected static T GetInstance()
    {
        if (instance != null)
        {
            return instance;
        }

        GameObject managerObj = new GameObject(typeof(T).Name);
        instance = managerObj.AddComponent<T>();
        DontDestroyOnLoad(managerObj);

        return instance;
    }
}
