using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class Signal : MonoBehaviour
    {
        #region -Declared Variables-

        protected Rigidbody2D rb;

        protected float horizontalInput;
        protected float verticalInput;
        
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;

        [Header("Ground Check Variables")]
        [SerializeField] protected Vector2 boxSize;
        [SerializeField] protected float castDistance;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected bool onGround;
        

        public bool OnGround
        {
            get => onGround;
            set => onGround = value;
        }

        [SerializeField] protected Sprite colorSprite;

        public Sprite ColorSprite
        {
            get => colorSprite;
            set => colorSprite = value;
        }
        
        [SerializeField] protected Sprite whiteSprite;
        
        public Sprite WhiteSprite
        {
            get => whiteSprite;
            set => whiteSprite = value;
        }
        
        [Header("Color Pick")]
        [SerializeField] protected Color32 inFieldColor;

        public Color32 InfieldColor
        {
            get => inFieldColor;
            set => inFieldColor = value;
        }
        [SerializeField] protected Color32 controlColor;

        public Color32 ControlColor
        {
            get => controlColor;
            set => controlColor = value;
        }
        [SerializeField] protected Color32 normalColor;

        public Color32 NormalColor
        {
            get => normalColor;
            set => normalColor = value;
        }

        [Header("Shaking Object by Rotation Variables")]
        [SerializeField] protected float rotationSpeed;
        [SerializeField] protected float shakeAmount;
        [SerializeField] protected float shakeDuration;

        protected Vector3 originRotation;
        protected float shakeTimer;
        
        #endregion

        private void Start()
        {
            originRotation = transform.eulerAngles;
        }

        protected virtual void GetInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            
            if(Input.GetKeyDown(KeyCode.W) && IsGrounded()
               || Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
                rb.velocity = Vector2.up * jumpForce;
        }

        private bool IsGrounded()
        {
            if (Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, castDistance, groundLayer))
            {
                return true;
            }
            
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        }

        protected virtual void Move()
        {
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

            if (horizontalInput < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (horizontalInput > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            transform.Translate(movement * speed * Time.deltaTime);

            // Rotate the player based on input
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationAmount);
            
            // Shake the player's rotation while moving
            
            if (Mathf.Abs(horizontalInput) > 0.1f && onGround)
            {
                if (shakeTimer <= 0)
                {
                    // Generate random offsets for the shake
                    float shakeOffsetX = Random.Range(-1f, 1f) * shakeAmount;
                    float shakeOffsetY = Random.Range(-1f, 1f) * shakeAmount;
                    
                    // Apply the shake offsets to the rotation
                    transform.eulerAngles = new Vector3(originRotation.x + shakeOffsetX,
                        originRotation.y + shakeOffsetY, rotationAmount);
                    
                    // Reset shake timer
                    shakeTimer = shakeDuration; //
                }
            
                shakeTimer -= Time.deltaTime;
            }
            else transform.rotation = Quaternion.Euler(originRotation); // Reset to the original rotation while not moving

            // transform.rotation = Quaternion.identity;
        }
    }
}
