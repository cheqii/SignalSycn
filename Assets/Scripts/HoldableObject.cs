using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HoldableObject : MonoBehaviour
{
    [SerializeField] private bool canHold;
    [SerializeField] private GameObject droneObj;

    [SerializeField] private bool canRelease;
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HoldObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DroneClaw"))
        {
            canHold = true;
            droneObj = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DroneClaw"))
        {
            canHold = false;
        }
    }

    void HoldObject()
    {
        if (droneObj == null) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canHold)
            {
                SoundManager.Instance.Play("DroneGrab");
                if (!canRelease)
                {
                    canRelease = true;
                    rb.gravityScale = 0f;
                    rb.mass = 0f;
                    droneObj.GetComponentInParent<Drone>().IsHolding = true;
                    gameObject.transform.SetParent(droneObj.transform);
                    print($"local pos : {gameObject.transform.localPosition}");
                }
                else
                {
                    canRelease = false;
                    rb.gravityScale = 1f;
                    rb.mass = 1f;
                    droneObj.GetComponentInParent<Drone>().IsHolding = false;
                    gameObject.transform.SetParent(null);
                }
            }
        }

        if (!droneObj.GetComponentInParent<Drone>().IsHolding || !droneObj.GetComponentInParent<Drone>().isSelected)
        {
            canRelease = false;
            rb.gravityScale = 1f;
            rb.mass = 1f;
            droneObj.GetComponentInParent<Drone>().IsHolding = false;
            gameObject.transform.SetParent(null);
        }
    }
}
