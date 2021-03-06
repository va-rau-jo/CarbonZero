﻿using UnityEngine;

namespace Assets.Scripts
{
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

        // clap config
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;

        private Vector3 velocity;
        private GridLockedPlacement placementScript;
        private GameManager gameManager;

        private void Awake()
        {
            velocity = Vector3.zero;
            placementScript = GameObject.FindGameObjectWithTag("UI").GetComponent<GridLockedPlacement>();

            GameObject gObject = GameObject.FindGameObjectWithTag("GameManager");
            if(gObject != null)
                gameManager = gObject.GetComponent<GameManager>();
        }

        void Update()
        {
            if(gameManager == null)
            {
                GameObject gObject = GameObject.FindGameObjectWithTag("GameManager");
                if (gObject != null)
                    gameManager = gObject.GetComponent<GameManager>();
            }
            else if (!placementScript.IsDragging() && gameManager.GameHasStarted())
            {
                if (Input.GetMouseButton(0))
                {
                    if (transform.rotation.y % 180 == 0)
                    {
                        int sf = transform.rotation.y == 0 ? -1 : 1;
                        velocity.x = sf * Input.GetAxis("Mouse X") * speed * Time.deltaTime;
                        velocity.z = sf * Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
                    }
                    else
                    {
                        int sf = transform.rotation.y == 0 ? -1 : 1;
                        velocity.z = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
                        velocity.x = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
                    }
                }
                else
                {
                    velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
                    velocity.z = Mathf.Clamp(velocity.z, -maxSpeed, maxSpeed);
                    velocity = (velocity.magnitude > 1) ? velocity - (velocity.normalized * acceleration) : velocity = Vector3.zero;
                }
                transform.position += velocity;
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, Mathf.Clamp(transform.position.z, yMin, yMax));
            }
        }
    }
}
