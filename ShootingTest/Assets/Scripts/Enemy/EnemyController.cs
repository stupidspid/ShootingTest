using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] private SpriteRenderer icon;

    private PlayerController _playerController;
    private BulletPool _bulletPool;
    
    [Inject]
    private void Construct(PlayerController playerController, BulletPool bulletPool)
    {
        _playerController = playerController;
        _bulletPool = bulletPool;
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
            Destroy(gameObject);
            _bulletPool.DespawnAll();
        }
    }
}
