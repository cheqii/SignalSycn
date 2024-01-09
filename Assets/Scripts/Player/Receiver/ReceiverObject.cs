using System;
using Unity.Mathematics;
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
            if (other.gameObject.CompareTag("Ground")) onGround = true;

            if (other.gameObject.CompareTag("PocketSignal")) onGround = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("SignalField"))
            {
                if (!isSelected) // check if receiver stay in field its will be yellow!
                {
                    gameObject.GetComponent<SpriteRenderer>().color = inFieldColor;
                    gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                }

                // if have another receiver exit field but selected receiver is still in field
                if (isSelected && GameController.Instance.isReceiver || isSelected && !GameController.Instance.isReceiver)
                {
                    GameController.Instance.isPocket = false;
                    GameController.Instance.isReceiver = true;
                    pocket.GetComponent<SpriteRenderer>().color = normalColor;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("SignalField"))
            {
                isSelected = false;
                pocket.pocketControl = true;
                
                if (pocket.pocketControl) // left receiver in field is exit from fields
                {
                    GameController.Instance.isPocket = true;
                    if (!GameController.Instance.isReceiver) pocket.GetComponent<SpriteRenderer>().color = controlColor;
                }

                if (!isSelected)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = normalColor;
                    gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                }

                if(!isSelected && !pocket.pocketControl)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = inFieldColor;
                    gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                }


                // transform.rotation = Quaternion.identity;
            }
        }

        #endregion

        public override void Move()
        {
            if(GameController.Instance.isReceiver && isSelected) base.Move();
        }
    }
}
