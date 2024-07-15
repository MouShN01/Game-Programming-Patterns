using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    [SerializeField] public bool isScaleble;
    [SerializeField] private int poolSize;
    [SerializeField] private Transform container;

    private List<T> pool;
    
    public PoolMono(T prefab, int size)
    {
        this.prefab = prefab;
        this.poolSize = size;
        this.container = null;

        this.CreatePool();
    }
    
    public PoolMono(T prefab, int size, Transform container)
    {
        this.prefab = prefab;
        this.poolSize = size;
        this.container = container;
        
        this.CreatePool();
    }

    private void CreatePool()
    {
        pool = new List<T>();

        for (int i = 0; i < poolSize; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
        {
            return element;
        }

        if (this.isScaleble)
        {
            return this.CreateObject(true);
        }

        throw new Exception("All objects are active now");
    }
}
