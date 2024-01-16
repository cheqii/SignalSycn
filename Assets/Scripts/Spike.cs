using System;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PocketSignal"))
        {
            Debug.Log("spike hurt");
            SoundManager.Instance.Play("TakeDamage");
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Receiver"))
        {
            SoundManager.Instance.Play("TakeDamage");
            Destroy(other.gameObject);
        }
    }
}
