using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PocketSignal : Signal
    {
        #region -Declared Variables-

        [SerializeField] private float signalRange = 5f;

        private CircleCollider2D realFieldCol;
        private CircleCollider2D signalFieldCol; // to check from receiverObj

        public List<ReceiverObject> receiverList;

        private List<float> receiverDist;


        [Header("Pocket Check")]
        [SerializeField] private bool isPocket;
        
        #endregion
        
        void Start()
        {
            isPocket = true;
            
            onGround = true;
            rb = GetComponent<Rigidbody2D>();

            realFieldCol = GetComponent<CircleCollider2D>();
            signalFieldCol = GetComponentInChildren<CircleCollider2D>();
            
            signalFieldCol.radius = realFieldCol.radius;
        }
        
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Receiver"))
            {
                receiverList.Add(other.gameObject.GetComponent<ReceiverObject>());
                other.GetComponent<SpriteRenderer>().color = new Color32(232, 255, 67, 255);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Receiver"))
            {
                receiverList.Remove(other.gameObject.GetComponent<ReceiverObject>());
                other.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        #region -Custom Function-

        void IncreaseFieldRadius(int value)
        {
            realFieldCol.radius += value;
            signalFieldCol.radius = realFieldCol.radius;
        }

        float FindReceiverDistance()
        {
            foreach (var receiver in receiverList)
            {
                var distance = Mathf.Sqrt(Mathf.Pow((receiver.transform.position.x - this.transform.position.x), 2)
                                          + Mathf.Pow((receiver.transform.position.y - this.transform.position.y), 2));
                receiverDist.Add(distance);
                
                if (receiver == null)
                {
                    receiverDist.Clear();
                }

                return distance;
            }

            return 0;
        }

        #endregion
    }
}
