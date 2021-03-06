﻿using UnityEngine;
using SharkInvador;
using System.Collections;


namespace SharkInvador
{
    public class Enemy : MonoBehaviour
    {

        #region Fields
        public float currentSpeed;              //the current speed of enemy
        public static float minSpeed = 1;                  //the minimum speed of enemy
        public static float maxSpeed = 2;                  //the maximum speed of enemy
        private float x, y, z;                  //coordinates of random new position
        public static float minSideways = -0.03f;                 //variable for the sideways direction of enemy
        public static float maxSideways = 0.03f;                 //variable for the sideways direction of enemy
        private float sideways;                 //variable for the sideways direction of enemy
        private float rotationX;                //random for the x axis rotation
        private float rotationY;                //random for the y axis rotation
        private float rotationZ;                //random for the z axis rotation
        private float MinRotateSpeed = 60f;     //variable for minimal rotation speed of enemy
        private float MaxRotateSpeed = 120f;    //variable for maximum rotation speed of enemy
        private float MinScale = 3f;           //variable for the minimal scale
        private float MaxScale = 5f;            //variable for the max scale
        private float currentRotationSpeed;     //current rotation speed
        public static float currentScaleX;      //current scale X axis
        public static float currentScaleY;      //current scale Y axis
        public static float currentScaleZ;      //current scale Z axis

        public Texture Banana1;
        public Texture Banana2;
        public Texture Banana3;
        private int selection;
        //private int count = 0;
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
            if (transform.position.x < -8.5f)
                transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
            else if (transform.position.x > 8.5f)
                transform.position = new Vector3(-8.5f, transform.position.y, transform.position.z);

        }







        //function to inizialize the positon and speed
        public void SetPositionAndSpeed()
        {
            
            selection = Random.Range(0, 3);
            switch (selection)
            {
                case (0):
                    renderer.material.mainTexture = Banana1;
                    break;

                case (1):
                    renderer.material.mainTexture = Banana2;
                    break;

                case (2):
                    renderer.material.mainTexture = Banana3;
                    break;
            }

            //get randoms for scale and rotation
            currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
            rotationX = Random.Range(-1f, 1f);
            rotationY = Random.Range(-1f, 1f);
            rotationZ = Random.Range(-1f, 1f);
            if (!BananaExploder.explode)
            {
                float scale = Random.Range(MinScale, MaxScale);
                currentScaleX = scale;
                currentScaleY = scale;
                currentScaleZ = scale;
            }
            else
            {
                float scale = Random.Range(1, 2);
                currentScaleX = scale;
                currentScaleY = scale;
                currentScaleZ = scale;
            }


            // to move the enemy into different directions
            minSideways -= 0.005f;
            maxSideways += 0.005f;
            sideways = Random.Range(minSideways, maxSideways);
            //set new speed
            minSpeed += 0.05f;
            maxSpeed += 0.5f;
            currentSpeed = Random.Range(minSpeed, maxSpeed);

            if (!BananaExploder.explode)
            {
                //set new position
                x = Random.Range(-9f, 9f);
                y = 7f;
                z = 0f;
                transform.position = new Vector3(x, y, z);
            }
            else
            {
                transform.position = BananaExploder.position;
                BananaExploder.explode = false;
            }

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