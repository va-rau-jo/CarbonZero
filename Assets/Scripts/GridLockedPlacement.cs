using UnityEngine;

public class GridLockedPlacement : MonoBehaviour
{
    private GameObject resource;
    public Vector3 placementRotation;
    public Vector3 localScale;
    public string resourceName;
    public float blueprintAlpha;
    Renderer resourceRenderer;
    private bool hovering;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hovering && Input.GetMouseButtonDown(0))
        {
            resource = SpawnResource();
            resourceRenderer = resource.GetComponent<Renderer>();
            SetResourceAlpha(blueprintAlpha);
        }

        if (resource != null && Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worlPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, this.transform.position.y));       // Fix Trig Calc for height
            resource.transform.position = new Vector3(Mathf.Round(worlPos.x / 5) * 5, 1.5f, Mathf.Round(worlPos.z / 5) * 5);
        }
         
        if (!hovering && Input.GetMouseButtonUp(0) && resource != null)
        {
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
}
