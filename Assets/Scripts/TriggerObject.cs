using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private GameObject getEffectObject;
    [SerializeField] private bool canTrigger;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                sprite.flipX = false;
                TriggerDestroyObject(getEffectObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Receiver") || other.CompareTag("PocketSignal"))
        {
            canTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Receiver") || other.CompareTag("PocketSignal"))
        {
            canTrigger = false;
        }
    }

    void TriggerDestroyObject(GameObject go)
    {
        Destroy(go, 0.5f);
    }
}
