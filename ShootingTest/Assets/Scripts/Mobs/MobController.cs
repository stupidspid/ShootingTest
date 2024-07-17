using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MobController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer icon;
    private PlayerController _playerController;
    private Vector3 _direction;
    private Rigidbody2D _rigidbody;
    private PoolObjects<MobController, MobFactory> _poolObjects;
    public float MovementSpeed { get; private set; }

    [Inject]
    private void Construct(PlayerController playerController, PoolObjects<MobController, MobFactory> poolObjects)
    {
        _playerController = playerController;
        _poolObjects = poolObjects;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetupMob(Mob mob, Vector3 position)
    {
        icon.sprite = mob.icon;
        MovementSpeed = mob.movementSpeed;

        transform.position = position;
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerController.transform.position, 
            MovementSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        _poolObjects.Despawn(this);
    }
}

public class MobFactory : PlaceholderFactory<MobController> { }
