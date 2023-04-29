using System;
using UnityEngine;
using UnityEngine.Pool;

namespace ferrouslights.PoolParty
{
    /// <summary>Base class for GameObjectPools</summary> 
    public abstract class PoolPartyBase<T> : MonoBehaviour
    where T : class
    {
        public T ObjectToPool;
        public bool CheckCollectionOnRelease;
        public int MaxPoolSize = 5;
        public int DefaultCapacity = 5;

        /// <summary>
        /// Returns a GameObject from the object pool
        /// </summary>
        /// <returns>The GameObject that would be instantiated</returns>
        public virtual T Get()
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
        
        private IObjectPool<T> _pool;

        /// <summary>
        /// Creates the pool if it hasn't been created yet
        /// </summary>
        public virtual IObjectPool<T> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                        OnDestroyPoolObject, CheckCollectionOnRelease, DefaultCapacity, MaxPoolSize);
                }

                return _pool;
            }
        }

        /// <summary>
        /// Creates an item if there are none ready in the pool
        /// </summary>
        /// <returns>The created item</returns>
        protected abstract T CreatePooledItem();

        /// <summary>
        /// Method for when the item is taken from the pool.
        /// </summary>
        /// <param name="outgoingObject"></param>
        protected abstract void OnTakeFromPool(T outgoingObject);

        /// <summary>
        /// Method for when the item is returned to the pool
        /// </summary>
        /// <param name="incomingObject"></param>
        protected abstract void OnReturnedToPool(T incomingObject);

        /// <summary>
        /// Method for when the object is destroyed
        /// </summary>
        /// <param name="destroyedObject"></param>
        protected abstract void OnDestroyPoolObject(T destroyedObject);
    }
}