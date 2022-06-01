using UnityEngine;
using UnityEngine.Pool;

namespace ferrouslights.PoolParty
{
    /// <summary>
    /// Basic expandable GameObject object pool for general use
    /// </summary>

    [AddComponentMenu("Pool Party GameObject Pool")]
    public class PoolParty : MonoBehaviour
    {
        public GameObject ObjectToPool;
        public bool CheckCollectionOnRelease;
        public int MaxPoolSize = 5;
        public int DefaultCapacity = 5;

        private IObjectPool<GameObject> _pool;
        public IObjectPool<GameObject> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, CheckCollectionOnRelease, DefaultCapacity, MaxPoolSize);
                }
                return _pool;
            }
        }

        /// <summary>
        /// Easy function for Pool.Get()
        /// </summary>
        /// <returns>A pooled object for reuse</returns>
        public GameObject Get()
        {
            return Pool.Get();
        }

        /// <summary>
        /// If an object is needed to be instantiated, run this code
        /// </summary>
        /// <returns>New object in pool</returns>
        private GameObject CreatePooledItem()
        {
            var newPooledObject = Instantiate(ObjectToPool);
            var returnToPool = newPooledObject.AddComponent<ReturnToPool>();
            returnToPool.Pool = Pool;

            return newPooledObject;
        }

        /// <summary>
        /// What happens when the object is returned to pool
        /// </summary>
        /// <param name="pooledObject">Returned object</param>
        private void OnReturnedToPool(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }

        /// <summary>
        /// What happens when the object is taken from the pool
        /// </summary>
        /// <param name="pooledObject">Taken object</param>
        private void OnTakeFromPool(GameObject pooledObject)
        {
            pooledObject.SetActive(true);
        }

        /// <summary>
        /// What happens when an object is destroyed from the pool
        /// </summary>
        /// <param name="pooledObject"></param>
        private void OnDestroyPoolObject(GameObject pooledObject)
        {
            Destroy(pooledObject);
        }
    }

    //// <summary>
    /// Interface for varying ReleaseToPool behaviors
    /// </summary>
    public interface ReleaseToPool
    {
        public void Release();
    }

    /// <summary>
    /// Attached to each instantiated object to allow for independent releasing
    /// </summary>
    public class ReturnToPool : MonoBehaviour, ReleaseToPool
    {
        public IObjectPool<GameObject> Pool;

        public void Release()
        {
            Pool.Release(gameObject);
        }
    }


}