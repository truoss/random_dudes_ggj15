using UnityEngine;
using SharkInvador;
using System.Collections;



namespace SharkInvador
{
    public class Stars : MonoBehaviour
    {

        public float speed;     //variable for the speed
        private int count = 0;





        // Update is called once per frame
        void Update()
        {
            //set the speed of the background
            float amtToMove = speed * Time.deltaTime;
            transform.Translate(Vector3.down * amtToMove, Space.World);

            //set back to top if background rached the bottom
            if (transform.position.y < -10.75f)
            {
                count++;
                if (count > 5)
                    speed = speed * 2;
                transform.position = new Vector3(transform.position.x, 14f, transform.position.z);
                
            }

        }
    }
}