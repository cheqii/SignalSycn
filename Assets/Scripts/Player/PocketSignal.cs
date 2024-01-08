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
        [SerializeField] private bool isPocket;
        public bool IsPocket
        {
            get => isPocket;
            set => isPocket = value;
        }

        public bool foundReceiver;
        [FormerlySerializedAs("switchControl")] public bool pocketControl;

        [Header("For Checking Switch Receiver")]
        public int switchCount = 0;
        public int tempCount = 2;
        
        #endregion
        
        void Start()
        {
            pocketControl = true;
            
            isPocket = true;
            
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

                switchCount = 0;
                tempCount = 0;
            }
        }

        #region -Custom Function-

        public override void Move()
        {
            if(isPocket) base.Move();
        }

        void IncreaseFieldRadius(int value)
        {
            signalFieldCol.radius += value;
        }

        void SwitchControlToReceiver()
        {
            if (foundReceiver && isPocket)
            {
                var pocketColor = gameObject.GetComponent<SpriteRenderer>();
                // var receiverSprite = receiverList[j].GetComponent<SpriteRenderer>();
                var triggerColor = new Color32(232, 255, 67, 255);
                var controlColor = new Color32(255, 76, 76 ,255);
                
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    receiverDist.Clear();

                    if (!pocketControl)
                    {
                        Debug.Log("checkkkk");
                        pocketColor.color = controlColor;
                        receiverList[tempCount].GetComponent<SpriteRenderer>().color = triggerColor;
                        pocketControl = true;
                    }
                    else if (switchCount < receiverList.Count && pocketControl)
                    {
                        Debug.Log("Switch Control to Receiver");
                        receiverList[tempCount].GetComponent<SpriteRenderer>().color = triggerColor;
                        receiverList[switchCount].GetComponent<SpriteRenderer>().color = controlColor;
                        pocketColor.color = Color.white;

                        receiverList[switchCount].GetComponent<ReceiverObject>().alreadySwitch = true;
                        tempCount = switchCount;
                        switchCount++;
                    }
                    
                    if (switchCount >= receiverList.Count)
                    {
                        Debug.Log("where is here");
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
