using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFabric<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    [SerializeField] private Transform pointToSpawn;
    private int _n = 0;

    public T GetNewInstance()
    {
        Vector3 pos = new Vector3(this.pointToSpawn.position.x, pointToSpawn.position.y,
            this.pointToSpawn.position.z - _n);
        _n++;
        return Instantiate(prefab, pos, Quaternion.identity);
    }
}
