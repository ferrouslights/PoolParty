using System;
using UnityEngine;
using UnityEngine.Pool;

namespace ferrouslights.PoolParty
{
    /// <summary>Base class for GameObjectPools</summary> 
    public abstract class SimpleGameObjectPoolPartyBase : MonoBehaviour
    {
        public GameObject ObjectToPool;
        public bool CheckCollectionOnRelease;
        public int MaxPoolSize = 5;
        public int DefaultCapacity = 5;

        /// <summary>
        /// Returns a GameObject from the object pool
        /// </summary>
        /// <returns>The GameObject that would be instantiated</returns>
        public virtual GameObject Get()
        {
            return Pool.Get();
        }

        /// <summary>
        /// Disposes of the pool when destroyed
        /// </summary>
        protected void OnDestroy()
        {
            if (_pool == null)
                return;
            if (_pool is IDisposable disposablePool)
                disposablePool.Dispose();
        }
        
        private IObjectPool<GameObject> _pool;

        /// <summary>
        /// Creates the pool if it hasn't been created yet
        /// </summary>
        public virtual IObjectPool<GameObject> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                        OnDestroyPoolObject, CheckCollectionOnRelease, DefaultCapacity, MaxPoolSize);
                }

                return _pool;
            }
        }

        /// <summary>
        /// Creates an item if there are none ready in the pool
        /// </summary>
        /// <returns>The created item</returns>
        protected virtual GameObject CreatePooledItem()
        {
            var newPooledObject = Instantiate(ObjectToPool);
            var returnToPool = newPooledObject.AddComponent<SimplePoolPartyReleaser>();
            returnToPool.Pool = Pool;
            returnToPool.TriggerAddedEvent();

            return newPooledObject;
        }

        /// <summary>
        /// Method for when the item is taken from the pool.
        /// </summary>
        /// <param name="outgoingObject"></param>
        protected virtual void OnTakeFromPool(GameObject outgoingObject)
        {
            outgoingObject.SetActive(true);
        }

        /// <summary>
        /// Method for when the item is returned to the pool
        /// </summary>
        /// <param name="incomingObject"></param>
        protected virtual void OnReturnedToPool(GameObject incomingObject)
        {
            incomingObject.SetActive(false);
        }

        /// <summary>
        /// Method for when the object is destroyed
        /// </summary>
        /// <param name="destroyedObject"></param>
        protected virtual void OnDestroyPoolObject(GameObject destroyedObject)
        {
            Destroy(destroyedObject);
        }
    }
    
    /// <summary>
    /// Base class for varying releasers. Override the pool base to change from simple to custom
    /// </summary>
    public abstract class PoolPartyReleaserBase : MonoBehaviour
    {
        public IObjectPool<GameObject> Pool { get; set; }
        public Action OnAddComponent;
        public Action OnRelease;
        public virtual void Release()
        {
            Pool.Release(gameObject);
            OnRelease?.Invoke();
        }

        public virtual void TriggerAddedEvent()
        {
            OnAddComponent?.Invoke();
        }
        
    }
}