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
    private PoolObjects<BulletController, BulletFactory> _bulletPool;
    private Coroutine _getDamageCoroutine;
    
    [Inject]
    private void Construct(PlayerController playerController, 
        PoolObjects<EnemyController, EnemyFactory> poolObjects,
        PoolObjects<BulletController, BulletFactory> bulletPool)
    {
        _playerController = playerController;
        _poolObjects = poolObjects;
        _bulletPool = bulletPool;
    }
    
    public int Health { get; private set; }
    public EnemyType EnemyType { get; private set; }
    public float MovementSpeed { get; private set; }

    private void OnEnable()
    {
        icon.color = Color.white;
    }

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

    private void SetupDamage(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.DOG:
                _getDamageCoroutine = StartCoroutine(GetDamageView());
                break;
            case EnemyType.MOUSE:
                MovementSpeed /= 2f;
                break;
        }
    }

    public void GetDamage(int damage)
    {
        SetupDamage(EnemyType);
        Health -= damage;
        _bulletPool.DespawnAll();
        if (Health <= 0)
        {
            _bulletPool.DespawnAll();
            _poolObjects.Despawn(this);
        }
    }

    private IEnumerator GetDamageView()
    {
        icon.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        icon.color = Color.white;
    }

    private void OnDisable()
    {
        if(_getDamageCoroutine!=null)
            StopCoroutine(_getDamageCoroutine);
    }
}
