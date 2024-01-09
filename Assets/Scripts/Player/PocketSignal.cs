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
                
                receiverList.Add(other.gameObject.GetComponent<ReceiverObject>());
                var sprite = other.GetComponent<SpriteRenderer>();

                sprite.color = inFieldColor;
                // sprite.sprite = other.GetComponent<ReceiverObject>().WhiteSprite;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Receiver"))
            {
                receiverList.Remove(other.gameObject.GetComponent<ReceiverObject>());
                other.GetComponent<SpriteRenderer>().color = normalColor;
                // other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<ReceiverObject>().WhiteSprite;

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
                                receiverList[tempCount].GetComponent<SpriteRenderer>().sprite = 
                                    receiverList[tempCount].GetComponent<ReceiverObject>().WhiteSprite;
                                receiverList[switchCount].GetComponent<SpriteRenderer>().color = normalColor;
                                receiverList[switchCount].GetComponent<SpriteRenderer>().sprite = 
                                    receiverList[switchCount].GetComponent<ReceiverObject>().ColorSprite;
                                
                                Debug.Log(receiverList[switchCount].GetComponent<SpriteRenderer>().sprite = receiverList[switchCount].GetComponent<ReceiverObject>().ColorSprite);

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

        float FindReceiverDistance()
        {
            foreach (var receiver in receiverList)
            {
                var distance = Mathf.Sqrt(Mathf.Pow((receiver.transform.position.x - this.transform.position.x), 2)
                                          + Mathf.Pow((receiver.transform.position.y - this.transform.position.y), 2));
                
                receiverDist.Add(distance);

                Debug.Log(distance);
                return distance;
            }
        
            return 0;
        }

        #endregion
    }
}
