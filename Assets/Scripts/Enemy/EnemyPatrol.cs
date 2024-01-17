using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Enemy _enemy;
    [SerializeField] private bool isPatrol;

    public bool IsPatrol
    {
        get => isPatrol;
        set => isPatrol = value;
    }

    [SerializeField] private bool isMovingRight;

    public bool IsMovingRight
    {
        get => isMovingRight;
        set => isMovingRight = value;
    }

    [SerializeField] private bool checkPatrol;

    public bool CheckPatrol
    {
        get => checkPatrol;
        set => checkPatrol = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isPatrol = true;
        isMovingRight = true;
        _enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    public void Patrol()
    {
        if (isPatrol)
        {
            _enemy.transform.Translate(Vector3.right * _enemy.Speed * Time.deltaTime);
            
            if (checkPatrol)
            {
                if (isMovingRight)
                {
                    _enemy.transform.eulerAngles = new Vector3(0, 180, 0);
                    isMovingRight = false;
                    checkPatrol = false;
                }
                else
                {
                    _enemy.transform.eulerAngles = new Vector3(0, 0, 0);
                    isMovingRight = true;
                    checkPatrol = false;
                }
            }
        }
    }
}
