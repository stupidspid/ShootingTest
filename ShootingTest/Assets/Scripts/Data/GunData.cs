using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomData/Gun", fileName = "Gun")]
public class GunData : ScriptableObject
{
    public List<Gun> guns = new();

    public Gun GetGunByType(GunType gunType)
    {
        return guns.FirstOrDefault(x => x.gunType == gunType);
    }
}

[Serializable]
public struct Gun
{
    public Sprite icon;
    public int bulletAmount;
    public int bulletDamage;
    public GunType gunType;
}

public enum GunType
{
    NONE = 0,
    GUN = 1,
    GUNSHOT = 2,
}
