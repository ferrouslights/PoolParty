using System;
using System.Collections;
using System.Collections.Generic;
using ferrouslights.PoolParty;
using UnityEngine;

public class CubeReleaser : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(ReleaseCube), 5f);
    }

    private void ReleaseCube()
    {
        if (gameObject.TryGetComponent<SimpleGameObjectPoolPartyReleaser>(out SimpleGameObjectPoolPartyReleaser releaser))
        {
            releaser.Release();
        }
    }
}
