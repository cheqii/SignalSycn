using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class RobotGunner : Enemy
{
    [Header("Robot Gunner Things")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject target;

    [SerializeField] private float fireRate;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // bulletPrefab.GetComponent<EnemyBullet>().CheckRight = false;
        
        speed = enemyData.speed;
        jumpForce = enemyData.jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            if (other.GetComponent<ReceiverObject>().isSelected)
            {
                foundPlayer = true;
                target = other.gameObject;
            }
        }
        
        if (other.CompareTag("PocketSignal"))
        {
            if (GameController.Instance.isPocket)
            {
                foundPlayer = true;
                target = other.gameObject;
            }
            else
            {
                foundPlayer = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            if (!other.GetComponent<ReceiverObject>().isSelected)
            {
                foundPlayer = false;
            }
            else
            {
                foundPlayer = true;
                target = other.gameObject;
            }
        }

        if (other.CompareTag("PocketSignal"))
        {
            if (other.CompareTag("PocketSignal"))
            {
                if (GameController.Instance.isPocket)
                {
                    foundPlayer = true;
                    target = other.gameObject;
                }
                else
                {
                    foundPlayer = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Receiver") || other.CompareTag("PocketSignal")) foundPlayer = false;
    }

    public override void AttackPlayer()
    {
        switch (foundPlayer)
        {
            case true:
            {
                FireBullet();
                break;
            }
        }
    }

    void FireBullet()
    {
        Debug.Log("Enemy Attack");
        var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        // bullet.transform.LookAt(target.transform.position);
        Destroy(bullet, 5f);
    }

    void CheckFaceDirection()
    {
        if (transform.rotation.y > 0)
        {
            bulletPrefab.GetComponent<EnemyBullet>().CheckRight = true;
        }
        if (transform.rotation.y <= 0) bulletPrefab.GetComponent<EnemyBullet>().CheckRight = false;
    }
}
