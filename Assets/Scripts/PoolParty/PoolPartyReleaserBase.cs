using System;
using UnityEngine;
using UnityEngine.Pool;

namespace ferrouslights.PoolParty
{
    /// <summary>
    /// Base class for varying releasers. Override the pool base to change from simple to custom
    /// </summary>
    public abstract class PoolPartyReleaserBase<T> : MonoBehaviour
        where T : class
    {
        public IObjectPool<T> Pool { get; set; }
        public Action OnReleaserAdded;
        public Action OnRelease;
        public T Object;
        public virtual void Release()
        {
            Pool.Release(Object);
            OnRelease?.Invoke();
        }

        public virtual void TriggerAddedEvent()
        {
            OnReleaserAdded?.Invoke();
        }
        
    }
}