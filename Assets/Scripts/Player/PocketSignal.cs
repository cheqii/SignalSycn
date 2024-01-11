using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PocketSignal : Signal
    {
        #region -Declared Variables-

        [SerializeField] private float signalRange = 5f;
        
        private CircleCollider2D signalFieldCol; // to check from receiverObj

        public List<ReceiverObject> receiverList;

        public List<float> receiverDist;


        [Header("Pocket Check")]
        public bool foundReceiver; 
        public bool pocketControl;

        [Header("For Checking Switch Receiver")]
        public int switchCount;
        public int tempCount;

        #endregion
        
        void Start()
        {

            pocketControl = true;
            
            onGround = true;
            
            rb = GetComponent<Rigidbody2D>();
            
            signalFieldCol = GetComponentInChildren<CircleCollider2D>();

            signalFieldCol.radius = signalRange;
        }
        
        void Update()
        {
            Move();
            SwitchControlToReceiver();
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
                Debug.Log("Trigger Receiver");
                foundReceiver = true;
                pocketControl = true;

                var receiver = other.GetComponent<ReceiverObject>();
                receiverList.Add(receiver);
                
                var sprite = other.GetComponent<SpriteRenderer>();
                if(!receiver.isSelected) sprite.color = inFieldColor;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Receiver"))
            {
                receiverList.Remove(other.gameObject.GetComponent<ReceiverObject>());
                other.GetComponent<SpriteRenderer>().color = normalColor;
               
                gameObject.GetComponent<SpriteRenderer>().color = controlColor;
                GameController.Instance.isPocket = true;
                GameController.Instance.isReceiver = false;
                
                switchCount = 0;
                tempCount = 0;
            }
        }

        #region -Custom Function-

        public override void Move()
        {
            if(GameController.Instance.isPocket) base.Move();
        }

        void IncreaseFieldRadius(int value)
        {
            signalFieldCol.radius += value;
        }

        void SwitchControlToReceiver()
        {
            if (foundReceiver)
            {
                var pocketColor = gameObject.GetComponent<SpriteRenderer>();
                
                foreach (var list in receiverList) // check drone is holding here if not hold then can switch a receiver
                {
                    if (list != null)
                    {
                        if (list.GetComponent<Drone>() == null)
                        {
                            break;
                        }
                        if (list.GetComponent<Drone>().IsHolding)
                        {
                            return; // if drone is holding then can't switch a receiver
                        }
                        if (list.GetComponent<ReceiverObject>())
                        {
                            break; // check if object is not drone then break a loop and can switch to another receiver
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    switch (pocketControl)
                    {
                        case true:
                        {
                            if (switchCount < receiverList.Count)
                            {
                                Debug.Log("Switch Control to Receiver");
                                GameController.Instance.isPocket = false;
                                GameController.Instance.isReceiver = true;

                                receiverList[tempCount].GetComponent<SpriteRenderer>().color = inFieldColor;
                                receiverList[tempCount].GetComponent<SpriteRenderer>().sprite = receiverList[tempCount].GetComponent<ReceiverObject>().WhiteSprite;
                                receiverList[switchCount].GetComponent<SpriteRenderer>().color = normalColor;
                                receiverList[switchCount].GetComponent<SpriteRenderer>().sprite = receiverList[switchCount].GetComponent<ReceiverObject>().ColorSprite;

                                pocketColor.color = normalColor;

                                receiverList[tempCount].GetComponent<ReceiverObject>().isSelected = false;
                                receiverList[switchCount].GetComponent<ReceiverObject>().isSelected = true;

                                tempCount = switchCount;
                                switchCount++;
                            }

                            if (switchCount >= receiverList.Count)
                            {
                                switchCount = 0;
                                pocketControl = false;
                            }

                            break;
                        }

                        case false:
                        {
                            Debug.Log("Check Return to Pocket Signal");
                            
                            GameController.Instance.isPocket = true;
                            GameController.Instance.isReceiver = false;
                            try
                            {
                                receiverList[tempCount].GetComponent<ReceiverObject>().isSelected = false;
                            }
                            catch (Exception e)
                            {
                                Debug.Log(e.Message);
                            }
                            pocketColor.color = controlColor;
                            try
                            {
                                receiverList[tempCount].GetComponent<SpriteRenderer>().color = inFieldColor; 
                                
                                receiverList[tempCount].GetComponent<SpriteRenderer>().sprite = 
                                    receiverList[tempCount].GetComponent<ReceiverObject>().WhiteSprite;
                            }
                            catch (Exception e)
                            {
                                Debug.Log(e.Message);
                            }

                            pocketControl = true;
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
