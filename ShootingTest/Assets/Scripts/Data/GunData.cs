using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomData/Gun", fileName = "Gun")]
public class GunData : ScriptableObject
{
    [SerializeField] private List<Gun> guns = new();
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
