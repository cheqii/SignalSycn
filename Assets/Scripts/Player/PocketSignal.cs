using System;
using UnityEngine;

namespace Player
{
    public class PocketSignal : Signal
    {
        #region -Declared Variables-

        [SerializeField] private float signalRange = 5f;

        [Header("Poket Check")]
        [SerializeField] private bool isPocket;
        
        #endregion
        
        void Start()
        {
            onGround = true;
            rb = GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            Move();
        }
        

        void FindReceiverInField()
        {
            
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
