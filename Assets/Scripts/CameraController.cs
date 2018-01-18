using UnityEngine;

public class CameraController : MonoBehaviour
{
    // multiplier by delta time to get a meaningful increase in velocity
    public float speed;

    // technically the deceleration, slows the camera down when there's no input
    public float acceleration;

    // the maximum speed the camera can be moving in one direction
    public float maxSpeed;

    // the edges of the terrain
    public float xBounds;
    public float zBounds;

    private Vector3 velocity;

    private void Awake()
    {
        velocity = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            velocity.x = -Input.GetAxis("Mouse X") * speed * Time.deltaTime;
            velocity.z = -Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
        }
        else
        {
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
            velocity.z = Mathf.Clamp(velocity.z, -maxSpeed, maxSpeed);
            velocity = (velocity.magnitude > 1) ? velocity - (velocity.normalized * acceleration) : velocity = Vector3.zero;
        }
        transform.position += velocity;
    }
}
