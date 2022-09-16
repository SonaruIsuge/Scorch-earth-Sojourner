
using UnityEngine;

namespace SonaruUtilities
{
    public class TSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        public static T Instance => GetInstance();

        private static T GetInstance()
        {
            if (instance  == null)
            {
                var tp = typeof(T);
                var obj = new GameObject(tp.Name);
                instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);
            }

            return instance;
        }


        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}

