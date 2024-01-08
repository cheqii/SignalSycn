
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ReceiverData", fileName = "Receiver", order = 0)]
public class ReceiverData : ScriptableObject
{
    public string name;
    public float speed;
    public float jumpForce;
    public float flyForce;

    public ReceiverType type;
}

public enum ReceiverType
{
    Attack,
    Support,
    None
}
