using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomData/Enemy", fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private List<Enemy> enemies = new ();
}

[Serializable]
public struct Enemy
{
    public Sprite icon;
    public int health;
    public EnemyType enemyType;
    public double movementSpeed;
}

public enum EnemyType
{
    NONE = 0,
    DOG = 1,
    MOUSE = 2,
}
