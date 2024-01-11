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

    public GameObject Target
    {
        get => target;
        set => target = value;
    }
    
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            receiverList.Add(other.GetComponent<ReceiverObject>());
            if (other.GetComponent<ReceiverObject>().isSelected)
            {
                Debug.Log($"Found {other.gameObject.name}");
                foundPlayer = true;
                target = other.gameObject;
            }
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            if (!other.GetComponent<ReceiverObject>().isSelected) foundPlayer = false;
            // if (receiverList.Count >= 2) return;
            for (int i = 0; i < receiverList.Count; i++)
            {
                if (receiverList[i].GetComponent<ReceiverObject>().isSelected)
                {
                    foundPlayer = true;
                    target = receiverList[i].gameObject;
                }
                if(GameController.Instance.isPocket)
                {
                    Debug.Log("SUpppp");
                    foundPlayer = false;
                    break;
                }
            }
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
        if (other.CompareTag("Receiver"))
        {
            receiverList.Remove(other.GetComponent<ReceiverObject>());
            foundPlayer = false;
        }
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
