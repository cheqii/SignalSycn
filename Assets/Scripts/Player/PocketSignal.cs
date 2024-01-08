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
        public int switchCount = 0;
        public int tempCount = 2;
        
        #endregion
        
        void Start()
        {
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
                foundReceiver = true;
                receiverList.Add(other.gameObject.GetComponent<ReceiverObject>());
                var sprite = other.GetComponent<SpriteRenderer>();

                sprite.color = new Color32(232, 255, 67, 255);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Receiver"))
            {
                receiverList.Remove(other.gameObject.GetComponent<ReceiverObject>());
                other.GetComponent<SpriteRenderer>().color = Color.white;

                gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 76, 76 ,255);
                pocketControl = false;
                GameController.Instance.isPocket = true;
                
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
                var triggerColor = new Color32(232, 255, 67, 255);
                var controlColor = new Color32(255, 76, 76 ,255);
                
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    receiverDist.Clear();

                    if (!pocketControl)
                    {
                        Debug.Log("Check Return to Pocket Signal");
                        GameController.Instance.isPocket = true;
                        GameController.Instance.isReceiver = false;
                        receiverList[tempCount].GetComponent<ReceiverObject>().isSelected = false;
                        pocketColor.color = controlColor;
                        try
                        {
                            receiverList[tempCount].GetComponent<SpriteRenderer>().color = triggerColor;
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                        }
                        pocketControl = true;
                    }
                    else if (switchCount < receiverList.Count && pocketControl)
                    {
                        Debug.Log("Switch Control to Receiver");
                        GameController.Instance.isPocket = false;
                        GameController.Instance.isReceiver = true;
                        receiverList[tempCount].GetComponent<SpriteRenderer>().color = triggerColor;
                        receiverList[switchCount].GetComponent<SpriteRenderer>().color = controlColor;
                        pocketColor.color = Color.white;

                        receiverList[switchCount].GetComponent<ReceiverObject>().isSelected = true;
                        receiverList[tempCount].GetComponent<ReceiverObject>().isSelected = false;
                        tempCount = switchCount;
                        switchCount++;
                    }
                    
                    if (switchCount >= receiverList.Count)
                    {
                        switchCount = 0;
                        pocketControl = false;
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
