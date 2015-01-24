﻿using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class BasePlayer : MonoBehaviour
    {
        public int xCoord;
        public int yCoord;
        public float speed = 5;
        //private Vector3 moveDir = Vector3.zero;
        float moveY = 0;
        float moveX = 0;
        public float maxSpeed = 10;
        internal float hInput;
        internal float vInput;

        Animator anim;

        void Awake()
        {
            anim = transform.GetChild(0).GetComponent<Animator>();
        }

        void Update()
        {
            if (GameLogic.I.curState != GameLogic.GameState.PLAYING)
                return;

            if (!GetUserInput())
                return;

            //UpdateViewDirection();                       

            //DetectAndApplyViewDirection();

        }

        internal void UpdateMoveDirection()
        {
            /*
            if (hInput > 0f)
                moveDir = new Vector3(1, 0, 0);//viewDir = ViewDir.Right; //x+
            else if (hInput < 0f)
                moveDir = new Vector3(-1, 0, 0);//viewDir = ViewDir.Left; //x-
            else if (vInput > 0f)
                moveDir = new Vector3(0, 1, 0);//viewDir = ViewDir.Up; //y+
            else if (vInput < 0f)
                moveDir = new Vector3(0, -1, 0); //y-
            else
                moveDir = Vector3.zero;
              */
        }

        void CalculateAndApplyMovement()
        {
            // Calculate actual motion
            Vector3 movement = new Vector2(hInput, vInput) * speed;
            //Vector3 movement = moveDir * speed;
            movement *= Time.deltaTime;

            //Debug.LogWarning(movement);
            Rotation(movement);

            //apply movement
            //this.transform.localPosition += movement;
            //rigidbody2D.AddForce(movement);
            rigidbody2D.MovePosition(rigidbody2D.position + new Vector2( movement.x, movement.y));


            //Rotation(movement);
            

            //Animation
            if (movement != Vector3.zero)
                anim.SetFloat("Speed", 1);
            else
                anim.SetFloat("Speed", 0);
        }

        private void Rotation(Vector3 movement)
        {
            //ausrichtung
            if (movement.x > 0 && transform.localScale != new Vector3(1, 1, 1))
                transform.localScale = new Vector3(1, 1, 1);
            else if (movement.x < 0 && transform.localScale != new Vector3(-1, 1, 1))
                transform.localScale = new Vector3(-1, 1, 1);
        }

        internal virtual bool GetUserInput()
        {
            return true;
        }

        void FixedUpdate()
        {
            if (GameLogic.I.curState != GameLogic.GameState.PLAYING)
                return;

            UpdateMoveDirection();

            CalculateAndApplyMovement(); 

            //movement
            if (Mathf.Abs(moveY) > Mathf.Abs(moveX))
            {                
                moveX = 0;
            }
            else
            {               
                moveY = 0;
            }

            rigidbody2D.velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
            //Debug.LogWarning(rigidbody2D.velocity);
           
        }       

    }
}

