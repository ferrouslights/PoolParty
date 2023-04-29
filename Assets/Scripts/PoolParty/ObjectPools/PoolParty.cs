using System;
using UnityEngine;

namespace ferrouslights.PoolParty
{
    /// <summary>
    /// Basic expandable GameObject object pool for general use
    /// </summary>
    [AddComponentMenu("Pool Party GameObject Pool")]
    public class PoolParty : PoolPartyBase<GameObject>
    {
        protected override GameObject CreatePooledItem()
        {
            var newPooledObject = Instantiate(ObjectToPool);
            var returnToPool = newPooledObject.AddComponent<SimpleGameObjectPoolPartyReleaser>();
            returnToPool.Pool = Pool;
            returnToPool.TriggerAddedEvent();

            return newPooledObject;
        }

        protected override void OnTakeFromPool(GameObject outgoingObject)
        {
            outgoingObject.SetActive(true);
        }

        protected override void OnReturnedToPool(GameObject incomingObject)
        {
            incomingObject.SetActive(false);
        }

        protected override void OnDestroyPoolObject(GameObject destroyedObject)
        {
            Destroy(destroyedObject);
        }
    }
}