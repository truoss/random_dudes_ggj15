using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class MoveMunchPipe : MonoBehaviour {

    public float speed;     //variable for the speed
    public float munchSpeed;
    public float time = 30f;

    private bool first = true;
    private bool second = true;
    public static bool third = true;

    


    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        Debug.LogWarning(time);
        //set the speed of the background
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.left * amtToMove, Space.World);

        //set back to top if background rached the bottom
        if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, 0f, transform.position.z);

        }

        if (time < 20 && first)
        {
            speed = speed +4;
            first = false;
        }
        if (time < 10 && second)
        {
            speed = speed +4;
            second = false;
        }
        if (time < 0 && third)
        {
            //nextgame
            if (MoveFlyingShark.Player1Shark)
            {
                MainUI.I.AddLeftPlayerScore();
                StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
            }
            else
            {
                MainUI.I.AddRightPlayerScore();
                StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadNextLevel));
            }
            third = false;
        }

        float amtToMoveV = 0;
        //moves the munch pipe verticaly
        if (transform.position.x < 11f && transform.position.x > 0f )
        {
            if (MoveFlyingShark.Player1Shark)
                amtToMoveV = Input.GetAxis("Vertical2") * munchSpeed * Time.deltaTime;
            else
                amtToMoveV = Input.GetAxis("Vertical") * munchSpeed * Time.deltaTime;

            transform.Translate(Vector3.up * amtToMoveV, Space.World);

            if(transform.position.y > 3f)
                transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
            if (transform.position.y < -3.5f)
                transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
        }

    }

    public IEnumerator Wait(int p, UnityAction action)
    {
        yield return new WaitForSeconds(p);

        action();
    }
}
