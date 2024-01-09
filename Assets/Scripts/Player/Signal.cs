using System;
using UnityEngine;

namespace Player
{
    public class Signal : MonoBehaviour
    {
        #region -Declared Variables-

        protected Rigidbody2D rb;
        
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;

        [SerializeField] protected bool onGround;

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
        [SerializeField] protected Color32 controlColor;
        [SerializeField] protected Color32 normalColor;
        
        #endregion

        public virtual void Move()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            // calculate movement direction
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

            if (horizontalInput == -1) gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (horizontalInput == 1) gameObject.GetComponent<SpriteRenderer>().flipX = false;
            
            transform.Translate(movement * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (onGround)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    onGround = false;
                }
            }
            
            transform.rotation = Quaternion.identity;
        }
    }
}
