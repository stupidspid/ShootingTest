using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GunUISelector : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    [SerializeField] private GunData gunData;
    private Toggle _toggle;
    private GunController _gunController;


    [Inject]
    private void Construct(GunController gunController)
    {
        _gunController = gunController;
    }

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(SelectNewGun);
    }

    private void SelectNewGun(bool isOn)
    {
        if (isOn)
        {
            var currentGunData = gunData.GetGunByType(gunType);
            _gunController.SetupGun(currentGunData);
        }
    }
}
