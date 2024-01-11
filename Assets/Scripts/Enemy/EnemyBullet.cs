using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletForce;
    
    private Rigidbody2D rb;
    private bool checkRight;

    public bool CheckRight
    {
        get => checkRight;
        set => checkRight = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!checkRight)
        {
            rb.velocity = Vector2.left * bulletForce;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
