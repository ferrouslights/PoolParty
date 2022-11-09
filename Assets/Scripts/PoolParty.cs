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

        public virtual GameObject Get()
        {
            return Pool.Get();
        }

        protected void OnDestroy()
        {
            if (_pool == null)
                return;
            if (_pool is IDisposable disposablePool)
                disposablePool.Dispose();
        }

        private IObjectPool<GameObject> _pool;

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

        protected virtual GameObject CreatePooledItem()
        {
            var newPooledObject = Instantiate(ObjectToPool);
            var returnToPool = newPooledObject.AddComponent<SimplePoolPartyReleaser>();
            returnToPool.Pool = Pool;

            return newPooledObject;
        }

        protected virtual void OnTakeFromPool(GameObject outgoingObject)
        {
            outgoingObject.SetActive(true);
        }

        protected virtual void OnReturnedToPool(GameObject incomingObject)
        {
            incomingObject.SetActive(false);
        }

        protected virtual void OnDestroyPoolObject(GameObject destroyedObject)
        {
            Destroy(destroyedObject);
        }
    }
    
    //// <summary>
    /// Interface for varying ReleaseToPool behaviors
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