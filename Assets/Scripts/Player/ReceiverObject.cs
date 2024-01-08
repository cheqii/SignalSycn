using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class ReceiverObject : Signal
    {

        #region -Declared Variables-
        
        public bool isSelected;

        #endregion
        
        #region -Unity Fuction-

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                onGround = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("SignalField"))
            {
                isSelected = false;
            }
        }

        #endregion

        public override void Move()
        {
            if(GameController.Instance.isReceiver && isSelected) base.Move();
        }
    }
}
