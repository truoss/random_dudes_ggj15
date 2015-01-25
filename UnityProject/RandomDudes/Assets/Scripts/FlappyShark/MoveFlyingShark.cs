using UnityEngine;
using System.Collections;
using UnityEngine.Events;



public class MoveFlyingShark : MonoBehaviour
{
    public float sharkSpeed;
    private bool move = true;
    public static int ranStart = 0;
    public static bool Player1Shark = false;


    void Start()
    {
        ranStart = Random.Range(0, 2);
        if (ranStart == 0)
        {
            Player1Shark = true;
            MainUI.I.SetLeftCharacter(MainUI.CharacterState.SHARK);
            MainUI.I.SetRightCharacter(MainUI.CharacterState.MUNCHPIPE);
        }
        else
        {
            Player1Shark = false;
            MainUI.I.SetLeftCharacter(MainUI.CharacterState.MUNCHPIPE);
            MainUI.I.SetRightCharacter(MainUI.CharacterState.SHARK);
        }

        if (MainUI.I)
            MainUI.I.SetCountDown(5, SharkWin);
    }

    public void SharkWin()
    {
        //nextgame
        if (MoveFlyingShark.Player1Shark)
        {
            MainUI.I.AddLeftPlayerScore();
            WinDialog.I.SetImageState(WinDialog.ImageState.LEFT);
            StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
        }
        else
        {
            MainUI.I.AddRightPlayerScore();
            WinDialog.I.SetImageState(WinDialog.ImageState.RIGHT);
            StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
        }
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMoveV = 0;
        float amtToMoveH = 0;
        if (move)
        {
            
            //moves the munch pipe verticaly
            if(Player1Shark)
                amtToMoveH = Input.GetAxis("Horizontal") * sharkSpeed * Time.deltaTime;
            else
                amtToMoveH = Input.GetAxis("Horizontal2") * sharkSpeed * Time.deltaTime;


            transform.Translate(Vector3.right * amtToMoveH, Space.World);

            if (transform.position.x < -8.5f)
                transform.position = new Vector3(-8.5f, transform.position.y, transform.position.z);
            if (transform.position.x > 0f)
                transform.position = new Vector3(0f, transform.position.y, transform.position.z);

            if (Player1Shark)
                amtToMoveV = Input.GetAxis("Vertical") * sharkSpeed * Time.deltaTime;
            else
                amtToMoveV = Input.GetAxis("Vertical2") * sharkSpeed * Time.deltaTime;


            transform.Translate(Vector3.up * amtToMoveV, Space.World);

            if (transform.position.y > 4f)
                transform.position = new Vector3(transform.position.x, 4f, transform.position.z);
            if (transform.position.y < -4f)
                transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
        }
        else
        {
            amtToMoveV = -1 * sharkSpeed * Time.deltaTime;
            transform.Translate(Vector3.up * amtToMoveV, Space.World);
        }

    }


    ////when player hits pipe
    void OnTriggerEnter(Collider otherObject)
    {        
        Debug.Log("Hit PIpe");
        move = false;
        //next game
        if (MoveMunchPipe.third)
        {
            if (Player1Shark)
            {
                MainUI.I.AddRightPlayerScore();
                WinDialog.I.SetImageState(WinDialog.ImageState.RIGHT);
                StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
            }
            else
            {
                MainUI.I.AddLeftPlayerScore();
                WinDialog.I.SetImageState(WinDialog.ImageState.LEFT);
                StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
            }
        }
    }

    public IEnumerator Wait(int p, UnityAction action)
    {
        yield return new WaitForSeconds(p);

        action();
    }
}







