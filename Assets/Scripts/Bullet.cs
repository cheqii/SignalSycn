using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletForce;
    private Vector3 mousePos;
    private Camera cam;
    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletForce;
        
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        
    }
}
