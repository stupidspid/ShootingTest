using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private int _minMobsSpawnAmount;
    [SerializeField] private int _maxMobsSpawnAmount;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private MobData _mobsData;
    [SerializeField] private float _spawnDelayTime;
    
    private PlayerController _playerController;
    private PoolObjects<MobController, MobFactory> _poolObjects;
    private MobFactory _mobFactory;
    
    [Inject]
    private void Constructor(MobFactory mobFactory,
        PoolObjects<MobController, MobFactory> poolObjects,
        PlayerController playerController)
    {
        _mobFactory = mobFactory;
        _poolObjects = poolObjects;
        _playerController = playerController;
    }

    private void Start()
    {
        _poolObjects.Register(_mobFactory, transform);
        StartCoroutine(SpawnMobs());
    }
    
    private IEnumerator SpawnMobs()
    {
        while (this)
        {
            var spawnAmount = Random.Range(_minMobsSpawnAmount, _maxMobsSpawnAmount);
            for (int i = 0; i < spawnAmount; i++)
            {
                var mob = _poolObjects.Spawn();
                var mobPosition = SpawnAroundService.GetNextPosition(spawnAmount,
                    i, _playerController.transform.position, _spawnRadius);
                var mobData = _mobsData.mobs[Random.Range(0, _mobsData.mobs.Count)];
            
                mob.SetupMob(mobData, mobPosition);
            }

            yield return new WaitForSeconds(_spawnDelayTime);
        }
    }
}
