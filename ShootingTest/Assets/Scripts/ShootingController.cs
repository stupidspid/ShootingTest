using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Button _shoot;
    
    private BulletFactory _bulletFactory;
    
    [Inject]
    private void Construct(BulletFactory bulletFactory)
    {
        _bulletFactory = bulletFactory;
    }

    private void Start()
    {
        _shoot.onClick.AddListener(OnShootClick);
    }

    private void OnShootClick()
    {
        _bulletFactory.Create();
    }
}

public class BulletFactory : PlaceholderFactory<BulletController>
{
    
}
