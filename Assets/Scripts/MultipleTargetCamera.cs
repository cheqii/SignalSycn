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
    private TriggerObject trigger;

    public bool switchCamToPocket;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        pocket = FindObjectOfType<PocketSignal>();
        trigger = FindObjectOfType<TriggerObject>();
        targetPlayer.Add(pocket.transform);
    }

    private void Update()
    {
        if (pocket.Booster == null) switchCamToPocket = false;
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (switchCamToPocket) switchCamToPocket = false;
                else switchCamToPocket = true;
            }
        }
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
        
        if (pocket.Booster != null)
        {
            if (pocket.Booster.IsActivated) cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, minZoom, Time.deltaTime);
        }

        if (pocket == null)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 12f, Time.deltaTime);
        }
    }

    Vector3 FollowPlayerSignal()
    {
        if (pocket == null) return Vector3.zero;
        
        if (trigger != null)
        {
            if (trigger.triggerWork && trigger.GetEffectObject != null)
                return trigger.GetEffectObject.transform.position;
        }
        if (pocket.Booster != null)
        {
            if (pocket.Booster.IsActivated && GameController.Instance.isPocket)
            {
                // if signal booster != null and activated then we can switch cam position between pocket and booster
                if (!switchCamToPocket)
                {
                    return pocket.Booster.transform.position;
                }

                if (switchCamToPocket)
                {
                    return pocket.transform.position;
                }
            }
        }

        if (!GameController.Instance.isPocket 
            && GameController.Instance.isReceiver 
            && GameController.Instance.isPocketDelay)
        {
            // if (pocket is delay after receiver is exit from field)
            return pocket.transform.position;
        }
        if (GameController.Instance.isPocket && pocket.Booster == null)
        {
            return pocket.transform.position;
        }
        if (GameController.Instance.isReceiver)
        {
            foreach (var list in pocket.receiverList)
            {
                if(list != null)
                {
                    if (list.isSelected)
                    {
                        return list.transform.position; // if  receiver is select return position to focus
                    }
                }
            }
        }
        return Vector3.zero;
    }
}
