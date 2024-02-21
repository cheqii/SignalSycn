using System;
using Player;
using UnityEngine;

public class Drone : ReceiverObject
{
    [SerializeField] private bool isHolding;
    
    [Header("Holdable Object")]
    public HoldableObject holdableObject;
    public bool IsHolding
    {
        get => isHolding;
        set => isHolding = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pocket = FindObjectOfType<PocketSignal>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")
            || other.gameObject.CompareTag("PocketSignal")
            || other.gameObject.CompareTag("Receiver"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")
            || other.gameObject.CompareTag("PocketSignal")
            || other.gameObject.CompareTag("Receiver"))
        {
            onGround = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SignalField"))
        {
            isSelected = false;
            isHolding = false;
            // pocket.pocketControl = true;
            
            if (pocket.pocketControl) // left receiver in field is exit from fields
            {
                GameController.Instance.isPocket = true;
                // GameController.Instance.isPocketDelay = true;
                // StartCoroutine(GameController.Instance.PlayerControllerDelay(GameController.Instance.pocketDelay));
                if (!GameController.Instance.isReceiver) pocket.GetComponent<SpriteRenderer>().color = controlColor;
            }

            if (!isSelected) 
            {
                gameObject.GetComponent<SpriteRenderer>().color = normalColor;
                gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
            }

            if(!isSelected && !pocket.pocketControl)
            {
                pocket.pocketControl = true;
                // GameController.Instance.isPocket = true;
                GameController.Instance.isPocketDelay = true;
                StartCoroutine(GameController.Instance.PlayerControllerDelay(GameController.Instance.pocketDelay));
                pocket.GetComponent<SpriteRenderer>().color = controlColor;
            }
                
        }
    }

    protected override void GetInput()
    {
        if (GameController.Instance.isReceiver && isSelected)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
    }

    protected override void Move()
    {
        if (GameController.Instance.isReceiver && isSelected)
        {
            rb.gravityScale = 0;

            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
            
            if (horizontalInput < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
            
            if (horizontalInput > 0) gameObject.GetComponent<SpriteRenderer>().flipX = false;

            if (isHolding)
            {
                holdableObject = transform.GetChild(0).GetComponentInChildren<HoldableObject>();
                
                if(holdableObject != null) 
                    holdableObject.transform.localPosition = Vector3.zero;
            }
            
            transform.Translate(movement * speed * Time.deltaTime);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
