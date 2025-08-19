using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform holdPoint)
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.freezeRotation = true;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        rb.useGravity = true;
        rb.freezeRotation = false;
        transform.SetParent(null);
    }

    public void MoveToHoldPoint(Vector3 targetPos)
    {
        rb.MovePosition(targetPos);
    }
}
