using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] protected EnemyData enemyData;

    public EnemyData EnemyData
    {
        get => enemyData;
        set => enemyData = value;
    }
        
    [Header("Stat")]
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected bool foundPlayer;

    public bool FoundPlayer
    {
        get => foundPlayer;
        set => foundPlayer = value;
    }

    protected Rigidbody2D rb;

    public virtual void MoveToPlayer()
    {
        
    }

    public virtual void AttackPlayer()
    {
        
    }
}
