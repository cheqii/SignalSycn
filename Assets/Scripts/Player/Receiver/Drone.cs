using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Drone : ReceiverObject
{
    private ReceiverObject receiver;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pocket = FindObjectOfType<PocketSignal>();
        receiver = GetComponent<ReceiverObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (GameController.Instance.isReceiver && isSelected)
        {
            rb.isKinematic = true;
            
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

            if (horizontalInput < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        
            if (horizontalInput > 0) gameObject.GetComponent<SpriteRenderer>().flipX = false;
        
            transform.Translate(movement * speed * Time.deltaTime);
        }
        else
        {
            rb.isKinematic = false;
        }
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        //
        // Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        //
        // if (horizontalInput < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //
        // if (horizontalInput > 0) gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //
        // transform.Translate(movement * speed * Time.deltaTime);
    }
}
