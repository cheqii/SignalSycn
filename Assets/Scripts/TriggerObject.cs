using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private GameObject getEffectObject;

    public GameObject GetEffectObject
    {
        get => getEffectObject;
        set => getEffectObject = value;
    }

    public bool triggerWork;
    
    [SerializeField] private bool canTrigger;
    private SpriteRenderer sprite;

    private int i = 0;

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
                SoundManager.Instance.Play("TriggerLever");
                sprite.flipX = false;
                triggerWork = true;
                TriggerDestroyObject(getEffectObject);
            }
        }

        if (i < 1) // play for one times
        {
            if (getEffectObject == null)
            {
                i++;
                SoundManager.Instance.Play("Destroy");
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
        Destroy(go, 2f);
    }
}
