using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targetPlayer;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    
    private Vector3 velocity;

    public float minZoom;
    public float maxZoom;

    private PocketSignal pocket;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        pocket = FindObjectOfType<PocketSignal>();
        targetPlayer.Add(pocket.transform);
    }

    private void LateUpdate()
    {
        CameraMovement();
        CameraZoom();
    }

    void CameraMovement()
    {
        Vector3 targetPos = FollowPlayerSignal();
        Vector3 newPos = targetPos + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }

    void CameraZoom()
    {
        if (GameController.Instance.isPocket) // if player control pocket signal then zoom cam out
        {
            // Debug.Log("Zoom out");
            float newZoom = Mathf.Lerp(maxZoom, minZoom, Time.deltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        }
        else // if not zoom in to focus receiver that selected
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, minZoom, Time.deltaTime);
        }
    }

    Vector3 FollowPlayerSignal()
    {
        if (GameController.Instance.isReceiver)
        {
            foreach (var list in pocket.receiverList)
            {
                if(list != null)
                {
                    if (list.isSelected)
                    {
                        // Debug.Log($"{list.gameObject.name} is target by cam");
                        return list.transform.position; // if  receiver is select return position to focus
                    }
                }
            }
        }
        
        return pocket.transform.position;
    }
}
