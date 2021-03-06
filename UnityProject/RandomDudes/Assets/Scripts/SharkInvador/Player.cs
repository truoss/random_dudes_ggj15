﻿using UnityEngine;
using System.Collections;
using SharkInvador;
using UnityEngine.Events;


namespace SharkInvador
{
    public class Player : MonoBehaviour
    {
        #region fields

        public static Player I;

        public float playerSpeed;                   //defines speed of the player
        public float projectileOffset;              //defines where the projectile starts
        public GameObject projectilePrefab;         //defines the object projectile
        public GameObject Projectile2Prefab;        //defines the object projectile
        public GameObject ExplosionPrefab;          //defines the explosions
        public GameObject EnemyPrefab;          //defines the explosions

        public GUIStyle backgroundStyle;
        public GUIStyle thumbStyle;

        private static float time = 60;

        private float shipInvisibleTime = 1.5f;     //time ship is invisible
        private float shipMoveOnToScreenSpeed = 5f; //speed for movement on screen
        private float blinkRate = .1f;              //the rate at which speed the ship blinks
        private int numberOfTimesToBlink = 10;      //how often ship blinks
        private int blinkCount;                     //to count the number of times the ship already blinked

        public static int ammoP1 = 5;             //ammo Player 1
        public static int ammoP2 = 5;             //ammo Player 2
        //public static int missed = 0;               //counts the number of asteroids missed
        //public static int score = 0;                //score of the player
        //public static int scoreAmmo = 0;            //score of the player
        public static bool receivedAmmo = false;    //if ammo was received
        public static int lives = 1;                //lives that are still remaining (player 1)
        public static int lives2 = 1;                //lives that are still remaining (player 2)

        //public static int klicks = 0;               //number of times shot (for debug)

        private bool first = true;
        private bool second = true;
        private float fire1Time = 2f;
        private float fire2Time = 2f;

        #endregion


        //enum for the states the player can be in
        enum State
        {
            Playing,
            Explosion,
            Invincible,
            Finished
        };

        private State state = State.Playing;        //sets the state at the beginning



        void Awake()
        {
            I = this;

            Init();
        }


        //coroutine for the Destroy ship
        IEnumerator DestroyShip()
        {
            //change player state to explosion
            state = State.Explosion;
            //play explosion
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            //change the position below the screen
            transform.position = new Vector3(0f, -8f, transform.position.z);
            //wait for a few seconds
            yield return new WaitForSeconds(shipInvisibleTime);

            //if player still has enough lives
            if (lives > 0 && lives2 > 0)
            {
                while (transform.position.y < -2f)
                {
                    //move the ship up
                    float amtToMove = shipMoveOnToScreenSpeed * Time.deltaTime;
                    transform.position = new Vector3(0, transform.position.y + amtToMove, transform.position.z);
                    yield return 0;
                }

                //now invincible
                state = State.Invincible;

                while (blinkCount < numberOfTimesToBlink)
                {
                    //lets the ship blink
                    gameObject.renderer.enabled = !gameObject.renderer.enabled;
                    if (gameObject.renderer.enabled)
                        blinkCount++;
                    yield return new WaitForSeconds(blinkRate);
                }

                //reset everything back to normal
                blinkCount = 0;
                //powerLive = 100;
                state = State.Playing;
            }
            //if player ran out of lives
            else
            {
                if (gameObject.tag == "Player")
                {
                    state = State.Finished;
                    if(GameLogic.curState == GameLogic.GameState.PLAYING)
                        GameLogic.I.SetState(GameLogic.GameState.RIGHTWINS);
                }
                if (gameObject.tag == "Player2")
                {
                    state = State.Finished;
                    if (GameLogic.curState == GameLogic.GameState.PLAYING)
                        GameLogic.I.SetState(GameLogic.GameState.LEFTWINS);
                }
            }

        }


        /*public IEnumerator Wait(int p, UnityAction action)
        {
            yield return new WaitForSeconds(p);

            action();
        }*/




        // Update is called once per frame
        void Update()
        {
            time -= Time.deltaTime ;
            fire1Time += Time.deltaTime;
            fire2Time += Time.deltaTime;

            if (time < 40 && first)
            {
                Instantiate(EnemyPrefab);
                first = false;
            }
            if (time < 20 && second)
            {
                Instantiate(EnemyPrefab);
                Instantiate(EnemyPrefab);
                second = false;
            }
            


            //only let player do something if he did not explode
            if (state != State.Finished && state != State.Explosion && GameLogic.curState == GameLogic.GameState.PLAYING)
            {
                /*
                if (time < 0)
                {
                    //lives = 0;
                    //lives2 = 0;
                    //StartCoroutine("DestroyShip");
                    //next game
                    state = State.Finished;
                    if (GameLogic.curState == GameLogic.GameState.PLAYING)
                        GameLogic.I.SetState(GameLogic.GameState.BOTHWIN);
                }
                */

                float amtToMoveV = 0;
                float amtToMoveH = 0;

                //Player 1
                if (gameObject.tag == "Player")
                {
                    // moves the player horizontaly

                    
                    amtToMoveH = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
                    
                    
                    transform.Translate(Vector3.right * amtToMoveH, Space.World);

                    //moves the player verticaly

                    amtToMoveV = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

                    transform.Translate(Vector3.up * amtToMoveV, Space.World);
                }

                //player 2
                if (gameObject.tag == "Player2")
                {
                    amtToMoveH = Input.GetAxis("Horizontal2") * playerSpeed * Time.deltaTime;


                    transform.Translate(Vector3.right * amtToMoveH, Space.World);

                    //moves the player verticaly

                    amtToMoveV = Input.GetAxis("Vertical2") * playerSpeed * Time.deltaTime;

                    transform.Translate(Vector3.up * amtToMoveV, Space.World);
                }


                //Debug.Log(Screen.width);
                //Debug.Log(transform.position);


                // ScreenWrap
                // lets the player reapear at the other side if the screeen horizontal
                if (transform.position.x < -8.5f)
                    transform.position = new Vector3(-8.5f, transform.position.y, transform.position.z);
                else if (transform.position.x > 8.5f)
                    transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
                // lets the player reapear at the other side if the screeen vertical
                if (transform.position.y < -3.5f)
                    transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
                else if (transform.position.y > 5.5f)
                    transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);





                //rotate
                //rotate player sideways when moving horizontally right
                if (amtToMoveH > 0)
                {
                    Quaternion target = Quaternion.Euler(-90, -30, 0);
                    transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * playerSpeed);

                }

                //rotate player sideways when moving horizontally left
                else if (amtToMoveH < 0)
                {
                    Quaternion target = Quaternion.Euler(-90, 30, 0);
                    transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * playerSpeed);
                }

                //rotate player sideways when moving forward
                else if (amtToMoveV < 0)
                {
                    Quaternion target = Quaternion.Euler(-120, 0, 0);
                    transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * playerSpeed);
                }

                //rotate player sideways when moving back
                else if (amtToMoveV > 0)
                {
                    Quaternion target = Quaternion.Euler(-60, 0, 0);
                    transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * playerSpeed);
                }

                // else go back to normal position
                else
                {
                    Quaternion target = Quaternion.Euler(-90, 0, 0);
                    transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * playerSpeed);
                }






                //shoot
                Shoot();

            }
        }




        public void Init()
        {
            time = 60;
            ammoP1 = 5;
            ammoP2 = 5;
            lives = 1;
            lives2 = 1;
            receivedAmmo = false;
            first = true;
            second = true;
            state = State.Playing;
            Enemy.minSideways = -0.03f;
            Enemy.maxSideways = 0.03f;
            Enemy.minSpeed = 1;
            Enemy.maxSpeed = 2;
            GameLogic.curState = GameLogic.GameState.PLAYING;

            if (MainUI.I)
                MainUI.I.SetCountDown(30, BothWin);
        }


        public void BothWin()
        {
            state = State.Finished;
            if (GameLogic.curState == GameLogic.GameState.PLAYING)
                GameLogic.I.SetState(GameLogic.GameState.BOTHWIN);
        }


#if UNITY_EDITOR
        //GUI funktion
        void OnGUI()
        {
            //Display Score
            GUI.Label(new Rect(10, Screen.height - 60, 200, 20), "Lives P1: " + Player.lives.ToString());

            //Display Lives
            GUI.Label(new Rect(Screen.width - 210, Screen.height - 60, 200, 20), "Lives P2: " + Player.lives2.ToString());

            //Display time remaining 
            GUI.Label(new Rect(Screen.width - 210, 10, 200, 20), Player.time.ToString());




            //convert power to a int
            // int iPowerLive = (int)Player.powerLive;
            //Display shield power selected
            //GUI.Label(new Rect(10, 170, 200, 20), "Energy: " + iPowerLive.ToString());


            //Display the ammo of the selected weapon

            //Display ammo for the selected weapon
            GUI.Label(new Rect(10, Screen.height - 40, 200, 20), "Ammo P1: " + Player.ammoP1.ToString());
            GUI.Label(new Rect(Screen.width - 210, Screen.height - 40, 200, 20), "Ammo P2: " + Player.ammoP2.ToString());
            //if no ammo, display no ammo
            if (ammoP1 < 1 || ammoP2 < 1)
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.fontSize = 35;
                style.normal.textColor = Color.red;
                style.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect((Screen.width - 200) / 2, (Screen.height - 20) / 2, 200, 40), "NO AMMO", style);
            }


            //display current number of missed enemys
            //GUI.Label(new Rect(10, 50, 120, 20), "Missed: " + missed.ToString());

            //display current number of missed enemys
            //GUI.Label(new Rect(10, 70, 120, 20), "Klicks: " + klicks.ToString());
        }
#endif





        ////when enemy hit player
        void OnTriggerEnter(Collider otherObject)
        {
            if (otherObject.tag == "Enemy" && state == State.Playing)
            {





                //Decrease the players life
                if (gameObject.tag == "Player")
                    Player.lives--;
                if (gameObject.tag == "Player2")
                    Player.lives2--;

                //Set a new position and speed for the hit enemy
                Enemy enemy = (Enemy)otherObject.gameObject.GetComponent("Enemy");
                enemy.SetPositionAndSpeed();

                //Instantiate the explosino
                StartCoroutine("DestroyShip");





            }


            if (otherObject.tag == "Ammo" && state == State.Playing && Player.receivedAmmo)
            {
                //get ammo
                if (gameObject.tag == "Player")
                    Player.ammoP1 += 10;
                if (gameObject.tag == "Player2")
                    Player.ammoP2 += 10;



                //got ammo
                Player.receivedAmmo = false;

                Ammo box = (Ammo)otherObject.gameObject.GetComponent("Ammo");

                //Explode ammo
                Instantiate(ExplosionPrefab, box.transform.position, box.transform.rotation);

                //Set a new position and speed for the hit ammo
                box.SetPositionAndSpeedAfter();



            }




        }






        //funktion to do everything connected with shooting
        void Shoot()
        {
            /*//Check for selected weapon
            if (Input.GetKeyDown("1"))
                weapon = 1;
            else if (Input.GetKeyDown("2"))
                weapon = 2;
            else if (Input.GetKeyDown("3"))
                weapon = 3;
            else if (Input.GetKeyDown("4"))
                weapon = 4;


            //selecting weapon with scollwheel
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                weapon++;
                if (weapon > 4)
                    weapon = 1;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                weapon--;
                if (weapon < 1)
                    weapon = 4;
            }*/

            
            //Player 1
            if (gameObject.tag == "Player")
            {
                // moves the player horizontaly

                if (Input.GetButtonDown("Fire1") && ammoP1 > 0 && fire1Time > 0.5)
                {
                    //Debug.LogWarning("Fire1: " + Input.GetButtonDown("Fire1"), this);
                    //determine position
                    Vector3 position = new Vector3(transform.position.x, transform.position.y + projectileOffset, transform.position.z);
                    //fire projectile
                    Instantiate(projectilePrefab, position, Quaternion.identity);
                    //subtract amunition
                    ammoP1--;
                    fire1Time = 0;
                }
            }
            //Player 2
            if (gameObject.tag == "Player2")
            {
                // moves the player horizontaly

                if (Input.GetButtonDown("Fire2") && ammoP2 > 0 && fire2Time > 0.5)
                {
                    //Debug.LogWarning("Fire2: " + Input.GetButtonDown("Fire2"), this);
                    //determine position
                    Vector3 position = new Vector3(transform.position.x, transform.position.y + projectileOffset, transform.position.z);
                    //fire projectile
                    Instantiate(projectilePrefab, position, Quaternion.identity);
                    //subtract amunition
                    ammoP2--;
                    fire2Time = 0;
                }
            }


            /*switch (weapon)
            {
                //select weapon
                case (1):    //Normal one shot
                    {
                        //determine position
                        Vector3 position = new Vector3(transform.position.x, transform.position.y + projectileOffset, transform.position.z);
                        //fire projectile
                        Instantiate(projectilePrefab, position, Quaternion.identity);

                        //subtract amunition
                        //ammoW1--;
                        break;
                    }
                case (2):   //Big Berta
                    {
                        if (ammoW2 > 0)
                        {
                            //determine position
                            Vector3 position = new Vector3(transform.position.x, transform.position.y + projectileOffset, transform.position.z);
                            //fire projectile
                            Instantiate(Projectile2Prefab, position, Quaternion.identity);

                            //subtract amunition
                            ammoW2--;
                        }
                        break;
                    }
                case (3):   //tripple shot
                    {
                        if (ammoW3 > 0)
                        {
                            //determine position
                            Vector3 position = new Vector3(transform.position.x, transform.position.y + projectileOffset, transform.position.z);
                            //fire projectile
                            Instantiate(projectilePrefab, position, Quaternion.identity);
                            position = new Vector3(position.x + 0.6f * transform.localScale.x, position.y, position.z);
                            Instantiate(projectilePrefab, position, Quaternion.identity);
                            position = new Vector3(position.x - 1.2f * transform.localScale.x, position.y, position.z);
                            Instantiate(projectilePrefab, position, Quaternion.identity);

                            //subtract amunition
                            ammoW3--;
                        }
                        break;
                    }
                case (4):   //trippleshot with angle
                    {
                        if (ammoW4 > 0)
                        {
                            //determine position
                            Vector3 position = new Vector3(transform.position.x, transform.position.y + projectileOffset, transform.position.z);
                            //fire projectile
                            Instantiate(projectilePrefab, position, Quaternion.identity);
                            Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, 30));
                            Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, -30));

                            //subtract amunition
                            ammoW4--;
                        }
                        break;
                    }

                */
            //////////////////////////
            //////////////////////////
            /////    TO   DO     /////
            //////////////////////////
            //////////////////////////
            //more weapons such as a bomb and guided misle

            //////////////////////////
            //////////////////////////
            /////    TO   DO     /////
            //////////////////////////
            //////////////////////////
            //ammo packages to refill ammo



        }
    }
}