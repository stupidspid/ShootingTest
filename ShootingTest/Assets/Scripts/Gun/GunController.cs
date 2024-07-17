using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform bulletSpawner;

    public int BulletAmount { get; private set; }
    public int BulletDamage { get; private set; }
    public GunType GunType { get; private set; }

    public Transform BulletSpawner => bulletSpawner;

    private void Start()
    {
        SetupGun(gunData.guns[0]);
    }

    public void SetupGun(Gun gunData)
    {
        icon.sprite = gunData.icon;
        BulletAmount = gunData.bulletAmount;
        BulletDamage = gunData.bulletDamage;
        GunType = gunData.gunType;
    }
}
