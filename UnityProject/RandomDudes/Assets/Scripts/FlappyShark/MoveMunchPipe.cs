using UnityEngine;
using System.Collections;

public class MoveMunchPipe : MonoBehaviour {

    public float speed;     //variable for the speed
    public float munchSpeed;




    // Update is called once per frame
    void Update()
    {
        //set the speed of the background
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.left * amtToMove, Space.World);

        //set back to top if background rached the bottom
        if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, 0f, transform.position.z);

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
