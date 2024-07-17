using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Button _shoot;

    private SignalBus _signalBus;
    
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Start()
    {
        _shoot.onClick.AddListener(OnShootClick);
    }

    private void OnShootClick()
    {
        _signalBus.Fire<ShootSignal>();
    }
}

public class BulletFactory : PlaceholderFactory<BulletController>
{
    
}
