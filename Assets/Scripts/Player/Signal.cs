using System;
using UnityEngine;

namespace Player
{
    public class Signal : MonoBehaviour
    {
        #region -Declared Variables-

        protected Rigidbody2D rb;
        
        [SerializeField] protected float speed = 5f;
        [SerializeField] protected float jumpForce = 2f;

        [SerializeField] protected bool onGround;

        #endregion

        public virtual void Move()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            // calculate movement direction
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

            transform.Translate(movement * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (onGround)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    onGround = false;
                }
            }
        }
    }
}
