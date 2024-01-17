using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class DrawFieldBound : MonoBehaviour
{
    private Transform spriteCircle;
    private PocketSignal pocket;

    public float radius;
    

    // Start is called before the first frame update
    void Start()
    {
        pocket = GetComponentInParent<PocketSignal>();
        spriteCircle = GetComponent<Transform>();
        radius = pocket.SignalRange;
        spriteCircle.localScale = new Vector3(radius * 2, radius * 2, 1f);
    }

    // Update is called once per frame

    void Update()
    {
        
    }


}
