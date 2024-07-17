using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField] private float targetingRadius;
    [SerializeField] private LayerMask targetLayer;

    private SignalBus _signalBus;
    private PoolObjects<BulletController,BulletFactory> _poolObjects;
    private GunController _gunController;
    private BulletFactory _bulletFactory;

    [Inject]
    private void Construct(SignalBus signalBus, 
        PoolObjects<BulletController,BulletFactory> poolObjects, 
        GunController gunController,
        BulletFactory bulletFactory)
    {
        _bulletFactory = bulletFactory;
        _poolObjects = poolObjects;
        _gunController = gunController;
        _signalBus = signalBus;
        _signalBus.Subscribe<ShootSignal>(ShootToEnemy);
    }

    private void ShootToEnemy()
    {
        var targetEnemy = GetNearestTarget();
        for(int i = 0; i < _gunController.BulletAmount; i++)
            _poolObjects.Spawn();
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