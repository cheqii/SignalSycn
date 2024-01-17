using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class SignalBooster : MonoBehaviour
{
    [SerializeField] private bool isActivated;

    public bool IsActivated
    {
        get => isActivated;
        set => isActivated = value;
    }

    [SerializeField] private Sprite whiteSprite;

    public Sprite WhiteSprite
    {
        get => whiteSprite;
        set => whiteSprite = value;
    }
    
    [SerializeField] private Sprite colorSprite;

    public Sprite ColorSprite
    {
        get => colorSprite;
        set => colorSprite = value;
    }

    [SerializeField] private GameObject signalField;

    private PocketSignal pocket;

    // Start is called before the first frame update
    void Start()
    {
        pocket = FindObjectOfType<PocketSignal>();
    }

    // Update is called once per frame
    void Update()
    {
        BoostSignal();
    }

    void BoostSignal()
    {
        if (isActivated)
        {
            signalField.SetActive(true);
        }
        else
        {
            signalField.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            Debug.Log("Found receiver by booster");
            pocket.receiverList.Add(other.GetComponent<ReceiverObject>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            Debug.Log("receiver exit in booster");
            other.GetComponent<ReceiverObject>().isSelected = false;
            other.GetComponent<ReceiverObject>().inField = false;
            other.GetComponent<SpriteRenderer>().color = Color.white;
            pocket.switchCount = 0;
            pocket.tempCount = pocket.switchCount;
            pocket.receiverList.Remove(other.GetComponent<ReceiverObject>());
        }
    }
}
