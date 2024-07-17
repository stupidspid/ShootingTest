using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private SignalBus _signalBus;
    private PoolObjects<BulletController,BulletFactory> poolObjects;
    private GunController _gunController;

    public Vector3 TargetPosition { get; set; }
    public Quaternion Rotation { get; set; }

    [Inject]
    private void Construct(SignalBus signalBus, PoolObjects<BulletController,BulletFactory> poolObjects, GunController gunController)
    {
        _signalBus = signalBus;
        this.poolObjects = poolObjects;
        _gunController = gunController;
    }

    private void OnEnable()
    {
        transform.position = _gunController.BulletSpawner.position;
        transform.rotation = Rotation == null ? Quaternion.identity : Rotation;
    }

    private void Update()
    {
        if (TargetPosition == null)
            return;
        
        transform.position = Vector3
            .MoveTowards(transform.position, TargetPosition,
                speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            gameObject.SetActive(false);
            enemy.GetDamage(_gunController.BulletDamage);
        }
    }

    private void OnBecameInvisible()
    {
        poolObjects.Despawn(this);
    }
}
