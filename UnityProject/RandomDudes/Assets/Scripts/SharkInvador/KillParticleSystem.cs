using UnityEngine;
using SharkInvador;
using System.Collections;


namespace SharkInvador
{
    public class KillParticleSystem : MonoBehaviour
    {


        // Update is called once per frame
        void Update()
        {
            //destroy particle
            if (!particleSystem.IsAlive())
                Destroy(gameObject);
        }
    }
}