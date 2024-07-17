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
    public float MovementSpeed { get; private set; }

    [Inject]
    private void Construct(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private void OnEnable()
    {
        _direction = (_playerController.transform.position - transform.position).normalized;
    }

    public void SetupMob(Mob mob, Vector3 position)
    {
        icon.sprite = mob.icon;
        MovementSpeed = mob.movementSpeed;

        transform.position = position;
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _direction,
                MovementSpeed * Time.deltaTime);
    }
}

public class MobFactory : PlaceholderFactory<MobController> { }
