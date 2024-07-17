using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _minEnemySpawnAmount;
    [SerializeField] private int _maxEnemySpawnAmount;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private float _spawnDelayTime;
    
    private PlayerController _playerController;
    private EnemyFactory _enemyFactory;
    private PoolObjects<EnemyController, EnemyFactory> _poolObjects;
    
    [Inject]
    private void Constructor(PlayerController playerController, 
        EnemyFactory enemyFactory,
        PoolObjects<EnemyController, EnemyFactory> poolObjects)
    {
        _playerController = playerController;
        _enemyFactory = enemyFactory;
        _poolObjects = poolObjects;
    }

    private void Start()
    {
        _poolObjects.Register(_enemyFactory, transform);
        StartCoroutine(SpawnMobs());
    }

    private IEnumerator SpawnMobs()
    {
        while (this)
        {
            var spawnAmount = Random.Range(_minEnemySpawnAmount, _maxEnemySpawnAmount);
            for (int i = 0; i < spawnAmount; i++)
            {
                var enemy = _poolObjects.Spawn();
                var enemyPosition = SpawnAroundService.GetNextPosition(spawnAmount,
                    i, _playerController.transform.position, _spawnRadius);
                var enemyData = _enemyData.enemies[Random.Range(0, _enemyData.enemies.Count)];
            
                enemy.SetupEnemy(enemyData, enemyPosition);
            }

            yield return new WaitForSeconds(_spawnDelayTime);
        }
    }
}

public class EnemyFactory : PlaceholderFactory<EnemyController>
{
    
}
