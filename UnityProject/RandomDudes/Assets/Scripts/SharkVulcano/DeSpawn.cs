using UnityEngine;
using System.Collections;

namespace SharkVulcano
{
    public class DeSpawn : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            //Debug.LogWarning(collision.gameObject.name);
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
