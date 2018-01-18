using UnityEngine;
using System.Collections;

public class MainMenuButtonScript : MonoBehaviour
{
    Animator anim;
    public bool hover;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hover == true)
        {
            Application.LoadLevel("Main");
        }
        Debug.Log("LOOP");
    }

    void OnMouseOver()
    {
        Debug.Log("Moused Over");
        hover = true;
        anim.SetBool("Hover", true);
        anim.SetBool("LeaveHover", false);
    }

    void OnMouseExit()
    {
        hover = false;
        anim.SetBool("Hover", false);
        anim.SetBool("LeaveHover", true);
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
