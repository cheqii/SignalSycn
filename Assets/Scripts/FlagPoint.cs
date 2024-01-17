using System;
using UnityEngine;

public class FlagPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PocketSignal"))
        {
            Debug.Log("Go to next stage");
            SoundManager.Instance.Play("NextLevel");
            GameController.Instance.NextLevelScene();
        }
    }
}
