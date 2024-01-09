using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class BreadMachine : MonoBehaviour
{
    private ReceiverObject receiver;
    
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject breadBullet;

    [SerializeField] private bool isShoot;

    public float shootForce;
    
    public UnityEvent receiverEvent;

    // Start is called before the first frame update
    void Start()
    {
        receiver = GetComponent<ReceiverObject>();
        shootPoint = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBread();
    }

    void ShootBread()
    {
        if (receiver.isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Shoot!!!");
                
                // breadBullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * shootForce;
                var bread = Instantiate(breadBullet, shootPoint.position, Quaternion.identity);
                bread.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(shootForce,0f), ForceMode2D.Impulse);
                Destroy(bread, 5f);
            }
        }
    }
    
}
