using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class Signal : MonoBehaviour
    {
        #region -Declared Variables-

        protected Rigidbody2D rb;
        
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;

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

        protected Vector3 originPos;
        
        #endregion

        private void Start()
        {
            originPos = transform.position;
        }

        public virtual void Move()
        {
            // // Rotate object around z axis
            // transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            //
            // if (shakeDuration > 0)
            // {
            //     // Generate random offset within a sphere and apply it to the object pos
            //     transform.position = originPos + Random.insideUnitSphere * shakeAmount;
            //     shakeDuration -= Time.deltaTime;
            // }
            // else
            // {
            //     // Reset to the original pos once the shake duration is over
            //     shakeDuration = 0f;
            //     // transform.position = originPos;
            // }

            float horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

            if (horizontalInput < 0)
            {
                // StartShake(0.5f);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (horizontalInput > 0)
            {
                // StartShake(0.5f);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            transform.Translate(movement * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (onGround)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    onGround = false;
                }
            }

            transform.rotation = Quaternion.identity;
        }

        public void StartShake(float duration)
        {
            shakeDuration = duration;
        }
    }
}
