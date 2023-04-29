using System;
using System.Collections;
using System.Collections.Generic;
using ferrouslights.PoolParty;
using UnityEngine;

[RequireComponent(typeof(PoolPartyBase<>))]
public class SpawnCube : MonoBehaviour
{
    private PoolPartyBase<GameObject> _prefabPool;

    private void Awake()
    {
        _prefabPool = GetComponent<PoolPartyBase<GameObject>>();
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
