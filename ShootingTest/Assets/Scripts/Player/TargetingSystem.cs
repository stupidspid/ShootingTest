using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField] private float targetingRadius;
    [SerializeField] private LayerMask targetLayer;

    private SignalBus _signalBus;
    private BulletPool _bulletPool;
    private GunController _gunController;

    [Inject]
    private void Construct(SignalBus signalBus, BulletPool bulletPool, GunController gunController)
    {
        _bulletPool = bulletPool;
        _gunController = gunController;
        _signalBus = signalBus;
        _signalBus.Subscribe<ShootSignal>(ShootToEnemy);
    }

    private void Start()
    {
        _bulletPool.RegisterBullet();
    }

    private void ShootToEnemy()
    {
        var targetEnemy = GetNearestTarget();
        _bulletPool.SpawnBullet(_gunController.BulletAmount);
        _signalBus.Fire(new ShootToEnemySignal(targetEnemy));
    }

    private Transform GetNearestTarget()
    {
        var targets = Physics2D.OverlapCircleAll((Vector2)transform.position, targetingRadius, targetLayer);
        if (targets.Length == 0) return null;

        return targets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).First().transform;
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ShootSignal>(ShootToEnemy);
    }
}