using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PocketSignal : Signal
    {
        #region -Declared Variables-

        [SerializeField] private float signalRange = 5f;

        public float SignalRange
        {
            get => signalRange;
            set => signalRange = value;
        }

        private CircleCollider2D signalFieldCol; // to check from receiverObj

        public List<ReceiverObject> receiverList;

        [Header("Pocket Check")] public bool foundReceiver;
        public bool pocketControl;

        [Header("For Checking Switch Receiver")]
        public int switchCount;
        public int tempCount;

        [Header("Camera")] 
        private MultipleTargetCamera cam;

        [Header("Signal Booster")]
        [SerializeField] private SignalBooster booster;

        public SignalBooster Booster
        {
            get => booster;
            set => booster = value;
        }

        #endregion

        void Start()
        {

            pocketControl = true;

            onGround = true;

            rb = GetComponent<Rigidbody2D>();

            signalFieldCol = GetComponentInChildren<CircleCollider2D>();

            signalFieldCol.radius = signalRange;

            cam = Camera.main.GetComponent<MultipleTargetCamera>();
        }

        void Update()
        {
            Move();
            SwitchControlToReceiver();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground")
                || other.gameObject.CompareTag("Receiver"))
            {
                onGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                onGround = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Receiver"))
            {
                print("Trigger Receiver");
                foundReceiver = true;
                pocketControl = true;

                var receiver = other.GetComponent<ReceiverObject>();
                receiverList.Add(receiver);
                cam.targetPlayer.Add(receiver.transform);

                var sprite = other.GetComponent<SpriteRenderer>();
                if (!receiver.isSelected) sprite.color = inFieldColor;
            }

            if (other.CompareTag("SignalBooster"))
            {
                SoundManager.Instance.Play("Found");
                booster = other.GetComponent<SignalBooster>();
                booster.IsActivated = true;
                booster.GetComponent<SpriteRenderer>().sprite = booster.ColorSprite;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Receiver"))
            {
                var receiver = other.GetComponent<ReceiverObject>();
                receiverList.Remove(receiver);
                cam.targetPlayer.Remove(receiver.transform);
                receiver.GetComponent<SpriteRenderer>().color = normalColor;

                gameObject.GetComponent<SpriteRenderer>().color = controlColor;
                // GameController.Instance.isPocket = true;
                // GameController.Instance.isReceiver = false;
                
                GameController.Instance.isPocketDelay = true;
                StartCoroutine(GameController.Instance.PlayerControllerDelay(GameController.Instance.pocketDelay));

                switchCount = 0;
                tempCount = 0;
            }
            
            if (other.CompareTag("SignalBooster"))
            {
                booster.IsActivated = false;
                booster.GetComponent<SpriteRenderer>().sprite = booster.WhiteSprite;

                booster = null;
            }
        }

        #region -Custom Function-

        public override void Move()
        {
            if (GameController.Instance.isPocket) base.Move();
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
                                print("Switch Control to Receiver");
                                SoundManager.Instance.Play("Switch");
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
                                SoundManager.Instance.Play("Switch");
                                switchCount = 0;
                                pocketControl = false;
                            }

                            break;
                        }

                        case false:
                        {
                            print("Check Return to Pocket Signal");
                            SoundManager.Instance.Play("Switch");
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
