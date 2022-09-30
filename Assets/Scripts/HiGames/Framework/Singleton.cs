using UnityEngine;

namespace HiGames.Framework
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }


                DontDestroyOnLoad(_instance);

                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
        }
    }
} 
