using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletForce;

    private Rigidbody2D rb;
    private Enemy enemyShooter;
    private Transform target;
    private PocketSignal pocket;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pocket = FindObjectOfType<PocketSignal>();
        enemyShooter = FindObjectOfType<RobotGunner>();
        target = enemyShooter.GetComponent<RobotGunner>().Target.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!enemyShooter.GetComponent<EnemyPatrol>().IsMovingRight)
        {
            Vector3 look = enemyShooter.transform.InverseTransformPoint(target.transform.position);
            rb.velocity = new Vector2(-look.x, look.y).normalized * bulletForce;
        }
        if (enemyShooter.GetComponent<EnemyPatrol>().IsMovingRight)
        {
            Vector3 look = enemyShooter.transform.InverseTransformPoint(target.transform.position);
            rb.velocity = new Vector2(look.x, look.y).normalized * bulletForce;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            if (other.GetComponent<ReceiverObject>().isSelected)
            {
                enemyShooter.FoundPlayer = false;
                other.GetComponent<ReceiverObject>().isSelected = false;
                pocket.pocketControl = true;
                GameController.Instance.isPocket = true;
                GameController.Instance.isReceiver = false;
                pocket.GetComponent<SpriteRenderer>().color = pocket.ControlColor;
                other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<ReceiverObject>().WhiteSprite;
                other.GetComponent<SpriteRenderer>().color = other.GetComponent<ReceiverObject>().InfieldColor;
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("PocketSignal"))
        {
            if (GameController.Instance.isPocket)
            {
                Debug.Log("get damage");
            }
            Destroy(gameObject);
        }
    }
}
