using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomData/Mob", fileName = "Mob")]
public class MobData : ScriptableObject
{
    public List<Mob> mobs = new();
}

[Serializable]
public struct Mob
{
    public Sprite icon;
    public float movementSpeed;
}