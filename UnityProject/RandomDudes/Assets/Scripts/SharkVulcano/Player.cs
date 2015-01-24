using UnityEngine;
using System.Collections;

namespace SharkVulcano
{
    public class Player : MonoBehaviour
    {
        public bool isLeft = true;
        public float speed = 5;
        //private Vector3 moveDir = Vector3.zero;
        //float moveY = 0;
        //float moveX = 0;
        public float maxSpeed = 10;
        internal float hInput;
        //internal float vInput;

        Animator anim;

        void Awake()
        {
            anim = transform.GetChild(0).GetComponent<Animator>();
            anim.SetBool("Dead", false);
        }

        void Update()
        {
            if (GameLogic.I.curState != GameLogic.GameState.PLAYING)
            {
                anim.SetFloat("Speed", 0);
                return;
            }                

            if (!GetUserInput())
                return;
        }

        bool GetUserInput()
        {
            //userinput
            if (isLeft)
                hInput = -Input.GetAxisRaw("Horizontal");
            else
                hInput = -Input.GetAxisRaw("Horizontal2");
            //vInput = Input.GetAxisRaw("Vertical");

            if (hInput != 0)
                return true;            

            return false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.LogWarning("blub: " + collision, collision.gameObject);
            /*
            if (collision.transform.GetComponent<PlayerShark>() != null)
                GameLogic.I.SetState(GameLogic.GameState.SHARKWINS);
             */ 
        }

        void CalculateAndApplyMovement()
        {
            // Calculate actual motion
            Vector3 movement = new Vector2(hInput, 0) * speed;
            //Vector3 movement = moveDir * speed;
            movement *= Time.deltaTime;

            //Debug.LogWarning(movement);
            //Rotation(movement);

            //apply movement
            //this.transform.localPosition += movement;
            //rigidbody2D.AddForce(movement);
            rigidbody.MovePosition(rigidbody.position + new Vector3(movement.x, movement.y));
            

            //Animation
            //Debug.LogWarning(hInput);
            if (hInput > 0)
                anim.SetFloat("Speed", 1);
            else if (hInput < 0)
                anim.SetFloat("Speed", -1);
            else
                anim.SetFloat("Speed", 0);
        }

        void FixedUpdate()
        {
            if (GameLogic.I.curState != GameLogic.GameState.PLAYING)
                return;

            //UpdateMoveDirection();

            CalculateAndApplyMovement();            

            //rigidbody.velocity = Vector2.zero;
            //Debug.LogWarning(rigidbody2D.velocity);

        }  

        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<Ball>() != null)
            {
                if (isLeft)
                    GameLogic.I.SetState(GameLogic.GameState.RIGHTWINS);
                else
                    GameLogic.I.SetState(GameLogic.GameState.LEFTWINS);

                //Destroy(this.gameObject);
                anim.SetBool("Dead", true);
            }
        }
    }
}
