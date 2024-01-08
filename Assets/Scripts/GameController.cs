using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Pocket Signal")]
    public bool isPocket;

    [Header("Receiver")]
    public bool isReceiver;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPocket = true;
        isReceiver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
