using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private int count = 3;
    [SerializeField] private bool isScaleble = false;
    [SerializeField] private TempObject tO;

    private PoolMono<TempObject> pool;

    private void Start()
    {
        this.pool = new PoolMono<TempObject>(this.tO, this.count, this.transform);
        this.pool.isScaleble = isScaleble;
    }

    public void CreateCube()
    {
        var rX = Random.Range(-5f, 5f);
        var rZ = Random.Range(-5f, 5f);
        var y = 0;

        Vector3 pos = new Vector3(rX, y, rZ);
        var cube = this.pool.GetFreeElement();
        cube.transform.position = pos;
    }
}
