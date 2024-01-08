using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class ReceiverObject : Signal
    {

        #region -Declared Variables-

        public ReceiverData receiverData;
        
        public bool isSelected;

        private PocketSignal pocket;

        #endregion
        
        #region -Unity Fuction-

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            pocket = FindObjectOfType<PocketSignal>();
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
                pocket.pocketControl = false;
                GameController.Instance.isPocket = true;
                pocket.GetComponent<SpriteRenderer>().color = new Color32(255, 76, 76 ,255);
            }
        }

        #endregion

        public override void Move()
        {
            if(GameController.Instance.isReceiver && isSelected) base.Move();
        }
    }
}
