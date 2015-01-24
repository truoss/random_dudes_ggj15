using UnityEngine;
using SharkInvador;
using System.Collections;


namespace SharkInvador
{
    public class Enemy : MonoBehaviour
    {

        #region Fields
        public float currentSpeed;              //the current speed of enemy
        public float minSpeed;                  //the minimum speed of enemy
        public float maxSpeed;                  //the maximum speed of enemy
        private float x, y, z;                  //coordinates of random new position
        private float minSideways = -0.05f;                 //variable for the sideways direction of enemy
        private float maxSideways = 0.05f;                 //variable for the sideways direction of enemy
        private float sideways;                 //variable for the sideways direction of enemy
        private float rotationX;                //random for the x axis rotation
        private float rotationY;                //random for the y axis rotation
        private float rotationZ;                //random for the z axis rotation
        private float MinRotateSpeed = 60f;     //variable for minimal rotation speed of enemy
        private float MaxRotateSpeed = 120f;    //variable for maximum rotation speed of enemy
        private float MinScale = .8f;           //variable for the minimal scale
        private float MaxScale = 2f;            //variable for the max scale
        private float currentRotationSpeed;     //current rotation speed
        public static float currentScaleX;      //current scale X axis
        public static float currentScaleY;      //current scale Y axis
        public static float currentScaleZ;      //current scale Z axis
        #endregion





        // Use this for initialization
        void Start()
        {

            //set starting position and speed
            SetPositionAndSpeed();

        }






        // Update is called once per frame
        void Update()
        {
            //set the rotation speed of enemy
            float rotationSpeed = currentRotationSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(rotationX, rotationY, rotationZ) * rotationSpeed);

            //move enemy
            float amtToMove = currentSpeed * Time.deltaTime;
            transform.Translate(Vector3.down * amtToMove, Space.World);
            transform.Translate(Vector3.right * sideways, Space.World);

            // ScreenWrap
            // lets the enemy reapear at the other side if the screeen horizontal
            if (transform.position.x < -9.3f)
                transform.position = new Vector3(9.3f, transform.position.y, transform.position.z);
            else if (transform.position.x > 9.3f)
                transform.position = new Vector3(-9.3f, transform.position.y, transform.position.z);

        }







        //function to inizialize the positon and speed
        public void SetPositionAndSpeed()
        {
            //get randoms for scale and rotation
            currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
            rotationX = Random.Range(-2f, 2f);
            rotationY = Random.Range(-2f, 2f);
            rotationZ = Random.Range(-2f, 2f);
            currentScaleX = Random.Range(MinScale, MaxScale);
            currentScaleY = Random.Range(MinScale, MaxScale);
            currentScaleZ = Random.Range(MinScale, MaxScale);


            // to move the enemy into different directions
            minSideways -= 0.01f;
            maxSideways += 0.01f;
            sideways = Random.Range(minSideways, maxSideways);
            //set new speed
            minSpeed += 0.05f;
            maxSpeed += 0.5f;
            currentSpeed = Random.Range(minSpeed, maxSpeed);

            //set new position
            x = Random.Range(-9f, 9f);
            y = 7f;
            z = 0f;
            transform.position = new Vector3(x, y, z);

            //set new scale
            transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);

        }








        //set new position and speed when enemy has reached the bottom of the screen
        void OnBecameInvisible()
        {
            SetPositionAndSpeed();
            //Player.missed++;
        }
    }
}