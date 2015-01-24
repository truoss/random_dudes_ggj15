using UnityEngine;
using System.Collections;

public class MoveFlyingShark : MonoBehaviour
{

    public float sharkSpeed;
    private bool move = true;


    // Update is called once per frame
    void Update()
    {
        float amtToMoveV = 0;
        if (move)
        {
            
            //moves the munch pipe verticaly

            amtToMoveV = Input.GetAxis("Vertical") * sharkSpeed * Time.deltaTime;

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

    }
}






