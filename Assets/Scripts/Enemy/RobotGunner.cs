using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;

public class RobotGunner : Enemy
{
    [Header("Receiver List")]
    [SerializeField] private List<ReceiverObject> receiverList;

    [Header("Robot Gunner Things")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject target;

    [SerializeField] private bool isFire;
    
    [SerializeField] private float fireRate;
    private float timer;
    
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

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Receiver"))
    //     {
    //         if (other.GetComponent<ReceiverObject>().isSelected)
    //         {
    //             Debug.Log($"Found {other.gameObject.name}");
    //             foundPlayer = true;
    //             target = other.gameObject;
    //         }
    //     }
    //     
    //     if (other.CompareTag("PocketSignal"))
    //     {
    //         if (GameController.Instance.isPocket)
    //         {
    //             Debug.Log($"Found {other.gameObject.name}");
    //             foundPlayer = true;
    //             target = other.gameObject;
    //         }
    //         else foundPlayer = false;
    //     }
    // }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            if (GameController.Instance.isReceiver)
            {
                Debug.Log("Hello world is it work ????");
                Debug.Log($"Found {other.gameObject.name}");
                foundPlayer = true;
                target = other.gameObject;
            }
            else foundPlayer = false;
        }

        if (other.CompareTag("PocketSignal"))
        {
            if (GameController.Instance.isPocket)
            {
                Debug.Log($"Found {other.gameObject.name}");
                foundPlayer = true;
                target = other.gameObject;
            }
            else foundPlayer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.CompareTag("Receiver") || other.CompareTag("PocketSignal")) foundPlayer = false;
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
        if (!isFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                isFire = true;
                timer = 0;
            }
        }
        else
        {
            Debug.Log("Enemy Attack");
            var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            Destroy(bullet, 1f);
            isFire = false;
        }
        
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
