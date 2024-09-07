using UnityEngine;

namespace Systems
{
    public abstract class Controller<T> : MonoBehaviour where T : Controller<T>
    {
        public static T Instance = null;
    
        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}
