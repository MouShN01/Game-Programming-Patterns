using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private ZombiFabric zombiFabric;
    [SerializeField] private SkeletonFabric skeletonFabric;

    public void SpawnZombi()
    {
        zombiFabric.GetNewInstance();
    }

    public void SpawnSkeleton()
    {
        skeletonFabric.GetNewInstance();
    }
}
