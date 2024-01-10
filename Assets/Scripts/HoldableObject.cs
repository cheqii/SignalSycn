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
        if (other.CompareTag("DroneClaw")) canHold = false;
    }

    void HoldObject()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canHold)
            {
                if (!canRelease)
                {
                    canRelease = true;
                    rb.isKinematic = true;
                    gameObject.transform.SetParent(droneObj.transform);
                }
                else
                {
                    canRelease = false;
                    rb.isKinematic = false;
                    gameObject.transform.SetParent(null);
                }
            }
        }
    }
}
