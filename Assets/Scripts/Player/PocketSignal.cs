using System;
using UnityEngine;

namespace Player
{
    public class PocketSignal : MonoBehaviour
    {
        #region -Declared Variables-

        private Rigidbody2D rb;
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 2f;

        [SerializeField] private bool onGround;
        
        [SerializeField] private float signalRange;

        #endregion
        // Start is called before the first frame update
        void Start()
        {
            onGround = true;
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        public void Move()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            
            // calculate movement direction
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

            transform.Translate(movement * speed * Time.deltaTime);

            float jump = jumpForce * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (onGround)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    onGround = false;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                onGround = true;
            }
        }
    }
}
