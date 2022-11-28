
using UnityEngine;

namespace SonaruUtilities
{
    public class TSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        public static T Instance => GetInstance();
        
        //private static bool applicationIsQuitting = false;
        

        private static T GetInstance()
        {
            //if (applicationIsQuitting) return null;
            
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
            //applicationIsQuitting = false;
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
        

        // public void OnDestroy()
        // {
        //     applicationIsQuitting = true;
        // }
    }
}

