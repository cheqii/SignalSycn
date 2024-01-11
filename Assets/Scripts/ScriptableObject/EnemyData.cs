using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData", fileName = "Enemy", order = 0)]
public class EnemyData : ScriptableObject
{
    public string name;
    public float speed;
    public float jumpForce;

    public EnemyType type;
}

public enum EnemyType
{
    Range,
    Melee
}
