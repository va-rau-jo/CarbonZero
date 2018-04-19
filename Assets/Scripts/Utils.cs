using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Utils : MonoBehaviour
    {
        public static float ExpConverge(float from, float to, float hl, float dTime)
        {
            return Mathf.Lerp(from, to, ExpQ(hl, dTime));
        }

        public static Vector3 ExpConverge(Vector3 from, Vector3 to, float hl, float dTime)
        {
            return Vector3.Lerp(from, to, ExpQ(hl, dTime));
        }

        public static float ExpQ(float hl, float dTime)
        {
            return 1f - Mathf.Pow(0.5f, dTime / Mathf.Max(hl, 0.0000001f));
        }
    }
}