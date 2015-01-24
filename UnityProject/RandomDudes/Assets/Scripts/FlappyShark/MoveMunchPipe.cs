using UnityEngine;
using System.Collections;

public class MoveMunchPipe : MonoBehaviour {

    public float speed;     //variable for the speed
    public float munchSpeed;
    public float time = 60f;

    private bool first = true;
    private bool second = true;




    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        //set the speed of the background
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.left * amtToMove, Space.World);

        //set back to top if background rached the bottom
        if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, 0f, transform.position.z);

        }

        if (time < 40 && first)
        {
            munchSpeed = munchSpeed * 2;
            first = false;
        }
        if (time < 20 && second)
        {
            munchSpeed = munchSpeed * 2;
            second = false;
        }
        if (time < 0 && first)
        {
            //nextgame
        }

        float amtToMoveV = 0;
        //moves the munch pipe verticaly
        if (transform.position.x < 9f && transform.position.x > 0f )
        {
            amtToMoveV = Input.GetAxis("Vertical2") * munchSpeed * Time.deltaTime;

            transform.Translate(Vector3.up * amtToMoveV, Space.World);

            if(transform.position.y > 3f)
                transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
            if (transform.position.y < -3.5f)
                transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
        }

    }
}
