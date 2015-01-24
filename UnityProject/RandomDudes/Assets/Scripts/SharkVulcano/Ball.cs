using UnityEngine;
using System.Collections;

namespace SharkVulcano
{
    public class Ball : MonoBehaviour
    {
        void OnCollistionEnter(Collision collision)
        {
            Debug.LogWarning(collision.gameObject.name);
        }
    }
}

