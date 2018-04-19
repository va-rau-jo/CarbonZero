using UnityEngine;

public class GridLockedPlacement : MonoBehaviour
{
    private GameObject resource;
    public Vector3 placementRotation;
    public Vector3 localScale;
    public Vector3 positionOffset;
    public string resourceName;
    public float gridLockSize;
    public float blueprintAlpha;
    Renderer resourceRenderer;

    private bool hovering;
    private bool dragging;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (hovering && Input.GetMouseButtonDown(0))
        {
            resource = SpawnResource();
            resourceRenderer = resource.GetComponent<Renderer>();
            SetResourceAlpha(blueprintAlpha);
            dragging = true;
        }

        if (resource != null && Input.GetMouseButton(0))
        {
            Vector3 dir = new Vector3(mousePos.x, 0, mousePos.y);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                resource.transform.position = new Vector3(Mathf.Round(hit.point.x / gridLockSize) * gridLockSize + positionOffset.x, positionOffset.y, Mathf.Round(hit.point.z / gridLockSize) * gridLockSize + positionOffset.z);
        }
                                                                                                                                    
        if (!hovering && Input.GetMouseButtonUp(0) && resource != null)
        {
            dragging = false;
            SetResourceAlpha(1.0f);
            resource = null;
        }
    }

    GameObject SpawnResource()
    {
        GameObject newResource = (GameObject)Instantiate(Resources.Load(resourceName));
        newResource.transform.localScale = localScale;
        newResource.transform.rotation = new Quaternion
        {
            eulerAngles = placementRotation
        };
        return newResource;
    }

    void OnMouseOver()
    {
        hovering = true;
    }

    void OnMouseExit()
    {
        hovering = false;
    }
                         
    void SetResourceAlpha(float alpha)
    {
        foreach (Material material in resourceRenderer.materials)
        {
            Color c = material.color;
            material.color = new Color(c.r, c.g, c.b, alpha);
        }
    }

    public bool IsDragging()
    {
        return dragging;
    }
}
