using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] private SpriteRenderer icon;

    private PlayerController _playerController;
    private PoolObjects<EnemyController, EnemyFactory> _poolObjects;
    
    [Inject]
    private void Construct(PlayerController playerController, PoolObjects<EnemyController, EnemyFactory> poolObjects)
    {
        _playerController = playerController;
        _poolObjects = poolObjects;
    }
    
    public int Health { get; private set; }
    public EnemyType EnemyType { get; private set; }
    public float MovementSpeed { get; private set; }
    
    public void SetupEnemy(Enemy enemy, Vector3 position)
    {
        icon.sprite = enemy.icon;
        Health = enemy.health;
        EnemyType = enemy.enemyType;
        MovementSpeed = enemy.movementSpeed;

        transform.position = position;
    }

    private void Update()
    {
        transform.position = Vector3
            .MoveTowards(transform.position, _playerController.transform.position,
                MovementSpeed * Time.deltaTime);
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            _poolObjects.Despawn(this);
        }
    }
}
