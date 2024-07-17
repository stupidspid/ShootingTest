using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletSpawner : MonoBehaviour
{
    private PoolObjects<BulletController, BulletFactory> _poolObjects;
    private BulletFactory _bulletFactory;
    
    [Inject]
    private void Construct(PoolObjects<BulletController,BulletFactory> poolObjects,
        BulletFactory bulletFactory)
    {
        _poolObjects = poolObjects;
        _bulletFactory = bulletFactory;
    }
    private void Start()
    {
        _poolObjects.Register(_bulletFactory, transform);
    }
}
