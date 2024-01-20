using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData", fileName = "Enemy", order = 0)]
public class EnemyData : ScriptableObject
{
    public Sprite colorSprite;
    public Sprite whiteSprite;
    
    public string enemyName;
    public float speed;
    public float jumpForce;

    public EnemyType type;
}

public enum EnemyType
{
    Range,
    Melee
}
