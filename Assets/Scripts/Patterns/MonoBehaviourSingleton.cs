using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private const string NOT_FOUND = "Not found instance for: ";
        private const string MAKE_NEW = "New one will be craeted.";
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.LogWarning(NOT_FOUND);
                        Debug.LogWarning(MAKE_NEW);
                        instance = new GameObject("Instance of: " + typeof(T)).AddComponent<T>();
                    }
                }

                return instance;
            }
        }
    }
}