using UnityEngine;

public class ItemOutline : MonoBehaviour
{
    public Material objectOutline;
    private MeshRenderer rend;
    private Material originalMaterial;
    private bool objectOutlined;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        if (rend != null)
        {
            originalMaterial = rend.material;
        }
    }

    public void ToggleOutline()
    {
        if (rend == null || objectOutline == null) return;

        // if (!objectOutlined)
        // {
            //needs to have both materials active at the same time
            rend.material = objectOutline;
            //objectOutlined = true;
        //}
        // else
        // {
        //     rend.material = originalMaterial;
        //     objectOutlined = false;
        // }
    }

    public void DisableOutline()
    {
        //if (objectOutlined)
        //{
            rend.material = originalMaterial;
            //objectOutlined = false;
        //}
    }
}
