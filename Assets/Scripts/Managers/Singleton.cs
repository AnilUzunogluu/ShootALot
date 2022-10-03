using System;
using UnityEngine;

namespace Managers
{
    public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static T Instance => GetInstance();
       
        private static T instance;

        private void OnEnable()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            GetInstance();
        }

        private static T GetInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                DontDestroyOnLoad(instance.gameObject);

            }

            if (instance != null)
            {
                return instance;
            }

            var obj = new GameObject(nameof(T));
            instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
            return instance;
        }
    }
    
    public abstract class SingletonProtected<T> : MonoBehaviour where T: MonoBehaviour
    {
        protected static T Instance => GetInstance();
       
        private static T instance;
        
        private void OnEnable()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            GetInstance();
        }

        private static T GetInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                DontDestroyOnLoad(instance.gameObject);
            }

            if (instance != null)
            {
                return instance;
            }

            var obj = new GameObject(nameof(T));
            instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
            return instance;
        }
    }
}