using UnityEngine;

namespace Assets.Scripts
{
    public class MiniMapManager : MonoBehaviour
    {
        private float worldSize = 1000;
        private float miniMapSize = 320;
        private new Camera camera;
        private Transform markerPos;

        private void Update()
        {
            SetMarkerPosition();
        }

        private void Awake()
        {
            camera = Camera.main;
            markerPos = transform.GetChild(0);
        }

        void SetMarkerPosition()
        {
            float offset = (miniMapSize / 2.0f) + 20;
            float x = (miniMapSize / worldSize) * camera.transform.position.x;
            float y = (miniMapSize / worldSize) * camera.transform.position.z;
            markerPos.transform.localPosition = new Vector3(x + offset, y - offset, 0);
        }
    }
}