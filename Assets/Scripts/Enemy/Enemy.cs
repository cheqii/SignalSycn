using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    
    [SerializeField] protected float jumpForce;
    
    [Header("Patrolling")]
    [SerializeField] protected bool foundPlayer;

    public bool FoundPlayer
    {
        get => foundPlayer;
        set => foundPlayer = value;
    }

    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void AttackPlayer()
    {
        
    }
}
