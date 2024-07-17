using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private PoolObjects<MobController, MobFactory> _poolMobs;
    private PoolObjects<EnemyController, EnemyFactory> _poolEnemies;

    [Inject]
    private void Construct(PoolObjects<MobController, MobFactory> poolMobs,
        PoolObjects<EnemyController, EnemyFactory> poolEnemies)
    {
        _poolEnemies = poolEnemies;
        _poolMobs = poolMobs;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<MobController>(out var mob))
        {
            _poolMobs.Despawn(mob);
        }
        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            _poolEnemies.Despawn(enemy);
        }
    }
    
}
