using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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
    private SpriteRenderer robotSprite;

    private EnemyPatrol _patrol;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        robotSprite = GetComponent<SpriteRenderer>();

        _patrol = GetComponent<EnemyPatrol>();
        
        speed = enemyData.speed;
        jumpForce = enemyData.jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        // CheckFaceDirection();
        AttackPlayer();
        if (foundPlayer) _patrol.IsPatrol = false;
        else _patrol.IsPatrol = true;
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
            robotSprite.sprite = enemyData.colorSprite;
            if (!other.GetComponent<ReceiverObject>().isSelected) foundPlayer = false;
            for (int i = 0; i < receiverList.Count; i++)
            {
                if (receiverList[i].GetComponent<ReceiverObject>().isSelected)
                {
                    foundPlayer = true;
                    target = receiverList[i].gameObject;
                    return;
                }
            }
            if(GameController.Instance.isPocket)
            {
                foundPlayer = false;
            }
        }

        if (other.CompareTag("PocketSignal"))
        {
            robotSprite.sprite = enemyData.colorSprite;
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
        _patrol.IsPatrol = true;
        if (other.CompareTag("Receiver") || other.CompareTag("PocketSignal"))
        {
            robotSprite.sprite = enemyData.whiteSprite;
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
            // Debug.Log("Enemy Attack");
            var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            Destroy(bullet, 1f);
            isFire = false;
        }
        
    }
}
