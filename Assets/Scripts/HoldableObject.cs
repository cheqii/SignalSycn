using UnityEngine;
using UnityEngine.Serialization;

public class HoldableObject : MonoBehaviour
{
    [SerializeField] private bool canHold;
    [SerializeField] private GameObject droneObj;

    [SerializeField] private bool canRelease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HoldThings();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DroneClaw"))
        {
            Debug.Log($"Drone can hold {gameObject.name}");
            canHold = true;
            droneObj = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DroneClaw")) canHold = false;
    }

    void HoldThings()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var rigidbody = gameObject.GetComponent<Rigidbody2D>();

            if (canHold)
            {
                if (!canRelease)
                {
                    canRelease = true;
                    rigidbody.isKinematic = true;
                    this.gameObject.transform.SetParent(droneObj.transform);
                }

                else if (canRelease)
                {
                    canRelease = false;
                    rigidbody.isKinematic = false;
                    this.gameObject.transform.SetParent(null);
                }
            }
        }
    }
}
