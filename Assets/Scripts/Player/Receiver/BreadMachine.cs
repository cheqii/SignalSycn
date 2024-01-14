using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class BreadMachine : ReceiverObject
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject breadBullet;

    // public UnityEvent receiverEvent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pocket = FindObjectOfType<PocketSignal>();
        
        shootPoint = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ShootBread();
    }

    public override void Move()
    {
        if(GameController.Instance.isReceiver && isSelected) base.Move();
    }

    void ShootBread()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Shoot!!!");
                SoundManager.Instance.Play("BreadBullet");
                var bread = Instantiate(breadBullet, shootPoint.position, shootPoint.rotation);
                
                Destroy(bread, 1f);
            }
        }
    }
}
