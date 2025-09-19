using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public GameObject reticleActive;
    public float pickupRange = 3f;
    [SerializeField] private Transform cameraTransform;

    void Start()
    {
        //set shader false
        reticleActive.SetActive(false);
    }
        void Update()
    {
         Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {

            if (hit.collider.CompareTag("Item"))
            {
                //set shader true
                reticleActive.SetActive(true);
                Debug.Log("Yep thats an item");

                var switcher = hit.collider.GetComponent<ItemOutline>();
                if (switcher != null)
                {
                    switcher.ToggleOutline();
                }
            }
            else
            {
                reticleActive.SetActive(false);
            }

        }
    }
}
