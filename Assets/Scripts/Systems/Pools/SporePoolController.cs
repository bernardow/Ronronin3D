using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporePoolController : MonoBehaviour
{
    public static SporePoolController Instance;
    
    public List<SporePoolUnit> SporePool = new List<SporePoolUnit>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    public SporePoolUnit GetSporeInPool()
    {
        for (int i = 0; i < SporePool.Count; i++)
        {
            if (!SporePool[i].InUse)
            {
                SporePool[i].gameObject.SetActive(true);
                return SporePool[i];
            }
        }
#if UNITY_EDITOR
        Debug.LogError("[SporePoolController] Spore not found in pool or all spore are in use", this);
#endif
        return null;
    }
}
