using System;
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
        // if(target == null) return;
        // if (!enemyShooter.GetComponent<EnemyPatrol>().IsMovingRight)
        // {
        //     Vector3 look = this.transform.InverseTransformPoint(target.transform.position);
        //     rb.velocity = new Vector2(-look.x, look.y).normalized * bulletForce;
        // }
        // if (enemyShooter.GetComponent<EnemyPatrol>().IsMovingRight)
        // {
        //     Vector3 look = this.transform.InverseTransformPoint(target.transform.position);
        //     rb.velocity = new Vector2(look.x, look.y).normalized * bulletForce;
        // }
        
    }

    private void LateUpdate()
    {
        if(target == null) return;
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
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Receiver"))
        {
            if (other.gameObject.GetComponent<ReceiverObject>().isSelected)
            {
                SoundManager.Instance.Play("TakeDamage");
                enemyShooter.FoundPlayer = false;
                other.gameObject.GetComponent<ReceiverObject>().isSelected = false;
                pocket.pocketControl = true;
                GameController.Instance.isPocket = true;
                GameController.Instance.isReceiver = false;
                pocket.GetComponent<SpriteRenderer>().color = pocket.ControlColor;
                other.gameObject.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<ReceiverObject>().WhiteSprite;
                other.gameObject.GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<ReceiverObject>().InfieldColor;
            }
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("PocketSignal"))
        {
            SoundManager.Instance.Play("TakeDamage");
            if (GameController.Instance.isPocket)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
