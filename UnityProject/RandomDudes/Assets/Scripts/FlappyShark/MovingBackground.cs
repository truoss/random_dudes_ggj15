using UnityEngine;
using System.Collections;

public class MovingBackground : MonoBehaviour {

    public float speed;     //variable for the speed





    // Update is called once per frame
    void Update()
    {
        //set the speed of the background
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.left * amtToMove, Space.World);

        //set back to top if background rached the bottom
        if (transform.position.x < -19.8f)
        {
            transform.position = new Vector3(18.5f, transform.position.y, transform.position.z);

        }

    }
}
