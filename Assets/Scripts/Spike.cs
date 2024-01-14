using System;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PocketSignal"))
        {
            Debug.Log("spike hurt");
            GameController.Instance.DecreaseLife(1);
        }

        if (other.gameObject.CompareTag("Receiver"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PocketSignal"))
        {
            Debug.Log("spike hurt");
            GameController.Instance.DecreaseLife(1);
        }
    }
}
