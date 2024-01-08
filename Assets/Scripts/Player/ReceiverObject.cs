using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class ReceiverObject : MonoBehaviour
    {

        #region -Declared Variables-


        [SerializeField] private bool isReceiver;

        public bool IsReceiver
        {
            get => isReceiver;
            set => isReceiver = value;
        }

        [FormerlySerializedAs("alreadySwitch")] public bool isSelected;

        #endregion
        
        #region -Unity Fuction-

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("SignalField"))
            {
                // if (alreadySwitch)
                // {
                //     Debug.Log("Already Switch");
                //     var sprite = gameObject.GetComponent<SpriteRenderer>();
                //     sprite.color = new Color32(232, 255, 67, 255);
                // }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("SignalField"))
            {
                isSelected = false;
            }
        }

        #endregion
    }
}
