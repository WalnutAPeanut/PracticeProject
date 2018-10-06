using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMProject
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject Obj = new GameObject();
                        instance = Obj.AddComponent<T>();
                        Obj.name = typeof(T).ToString();
                    }
                    if (instance == null)
                    {
                        Debug.LogError("single is't create" + typeof(T).ToString());
                    }
                }
                return instance;
            }
        }
    }
}