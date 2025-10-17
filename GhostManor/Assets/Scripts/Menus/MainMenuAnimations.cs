using UnityEngine;

public class MainMenuAnimations : MonoBehaviour
{
    public float moveAmount = 0.5f;
    public float smoothSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float moveZ = (Input.mousePosition.x / Screen.width - 0.5f) * moveAmount;
        float moveY = (Input.mousePosition.y / Screen.height - 0.5f) * moveAmount;

        Vector3 targetPos = startPos + new Vector3(0, moveY, -moveZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
