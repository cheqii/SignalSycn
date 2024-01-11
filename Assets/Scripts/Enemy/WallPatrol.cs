using UnityEngine;

public class WallPatrol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyPatrol>().CheckPatrol = true;
        }
    }
}
