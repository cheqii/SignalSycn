using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class ReceiverObject : Signal
    {

        #region -Declared Variables-

        public bool isSelected;

        public bool inField;

        protected PocketSignal pocket;

        #endregion
        
        #region -Unity Fuction-

        // Start is called before the first frame update
        void Start()
        {
            originPos = transform.position;
            rb = GetComponent<Rigidbody2D>();
            pocket = FindObjectOfType<PocketSignal>();
        }

        private void Update()
        {
            if(GameController.Instance.isReceiver && isSelected) Move();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("PocketSignal")
                || other.gameObject.CompareTag("Receiver")) 
                onGround = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("SignalField"))
            {
                inField = true;
                
                if (!isSelected && inField) // check if receiver stay in field its will be yellow!
                {
                    gameObject.GetComponent<SpriteRenderer>().color = inFieldColor;
                    gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                }

                if (!isSelected && !inField)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = normalColor;
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
                inField = false;

                if (pocket.pocketControl) // left receiver in field is exit from fields
                {
                    GameController.Instance.isPocket = true;
                    // GameController.Instance.isPocketDelay = true;
                    // StartCoroutine(GameController.Instance.PlayerControllerDelay(GameController.Instance.pocketDelay));
                    if (!GameController.Instance.isReceiver) pocket.GetComponent<SpriteRenderer>().color = controlColor;
                }

                if (!isSelected) // if object is not select but still in field
                {
                    inField = false;
                    gameObject.GetComponent<SpriteRenderer>().color = normalColor;
                    gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                }

                if(!isSelected && !pocket.pocketControl) // if controlling is out of field
                {
                    // gameObject.GetComponent<SpriteRenderer>().color = inFieldColor;
                    // gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                    
                    pocket.pocketControl = true;
                    // GameController.Instance.isPocket = true;
                    GameController.Instance.isPocketDelay = true;
                    StartCoroutine(GameController.Instance.PlayerControllerDelay(GameController.Instance.pocketDelay));
                    pocket.GetComponent<SpriteRenderer>().color = controlColor;
                }
                
            }
        }

        #endregion
    }
}
