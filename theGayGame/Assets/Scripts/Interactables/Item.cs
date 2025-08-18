using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Rigidbody rb;

    public string itemName;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform holdPoint)
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;

        transform.SetParent(null);
    }

    public void MoveToHoldPoint(Vector3 targetPos)
    {
        rb.MovePosition(targetPos);
    }
}
