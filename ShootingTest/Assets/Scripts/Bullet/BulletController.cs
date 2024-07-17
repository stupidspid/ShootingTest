using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Transform _targetTransform;
    private SignalBus _signalBus;
    private BulletPool _bulletPool;
    private GunController _gunController; 

    public bool IsActive
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    public Quaternion Rotation { get; set; }

    [Inject]
    private void Construct(SignalBus signalBus, BulletPool bulletPool, GunController gunController)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<ShootToEnemySignal>(GetShootParams);
        _bulletPool = bulletPool;
        _gunController = gunController;
    }

    private void OnEnable()
    {
        transform.position = _gunController.BulletSpawner.position;
        transform.rotation = Rotation == null ? Quaternion.identity : Rotation;
    }

    private void Update()
    {
        if (_targetTransform == null)
            return;
        
        transform.position = Vector3
            .MoveTowards(transform.position, _targetTransform.position,
                speed * Time.deltaTime);
    }
    
    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ShootToEnemySignal>(GetShootParams);
    }

    private void GetShootParams(ShootToEnemySignal shootSignal)
    {
        if (shootSignal.Target == null)
            return;
        
        _targetTransform = shootSignal.Target;
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
        _bulletPool.DespawnBullet(this);
    }
}
