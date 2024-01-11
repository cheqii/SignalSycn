using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletForce;
    
    private Rigidbody2D rb;
    private bool checkRight;

    public bool CheckRight
    {
        get => checkRight;
        set => checkRight = value;
    }

    private PocketSignal pocket;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pocket = FindObjectOfType<PocketSignal>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!checkRight)
        {
            rb.velocity = Vector2.left * bulletForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            if (other.GetComponent<ReceiverObject>().isSelected)
            {
                other.GetComponent<ReceiverObject>().isSelected = false;
                pocket.pocketControl = true;
                GameController.Instance.isPocket = true;
                GameController.Instance.isReceiver = false;
                pocket.GetComponent<SpriteRenderer>().color = pocket.ControlColor;
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
