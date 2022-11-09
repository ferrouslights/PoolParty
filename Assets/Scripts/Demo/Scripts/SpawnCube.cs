using System;
using System.Collections;
using System.Collections.Generic;
using ferrouslights.PoolParty;
using UnityEngine;

[RequireComponent(typeof(SimpleGameObjectPoolPartyBase))]
public class SpawnCube : MonoBehaviour
{
    private SimpleGameObjectPoolPartyBase _prefabPool;

    private void Awake()
    {
        _prefabPool = GetComponent<SimpleGameObjectPoolPartyBase>();
    }

    public void TriggerSpawnCube()
    {
        if (_prefabPool == null)
        {
            Debug.LogWarning("Pool missing!");
            return;
        }
        GameObject spawnedCube = _prefabPool.Get();
        spawnedCube.transform.SetAsLastSibling();
        spawnedCube.transform.position = transform.position;
    }
}
